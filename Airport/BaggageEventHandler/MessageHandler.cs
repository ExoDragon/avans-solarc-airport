using BaggageEventHandler.Events;
using BaggageEventHandler.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageEventHandler
{
    static class MessageHandler
    {
        public static void RabbitMQMessageHandler(this IServiceCollection services, IConfiguration config)
        {
            var serviceProvider = services.BuildServiceProvider();
            var factory = new ConnectionFactory() { HostName = config["RabbitMQHandler:Host"], UserName = config["RabbitMQHandler:Username"], Password = config["RabbitMQHandler:Password"] };
            
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: config["RabbitMQHandler:Exchange"], type: "topic");
            var queueName = channel.QueueDeclare(
                                queue: config["RabbitMQHandler:Queue"],
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null
                            ).QueueName;

            var index = 0;
            while (config[$"RabbitMQHandler:RoutingKeys:{index}:RoutingKey"] != null)
            {
                channel.QueueBind(queue: queueName, exchange: config["RabbitMQHandler:Exchange"], routingKey: config[$"RabbitMQHandler:RoutingKeys:{index}:RoutingKey"]);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;

                    Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);

                    var type = Type.GetType($"BaggageEventHandler.Events.{routingKey}");
                    var repository = serviceProvider.GetService<IBaggageRepository>();
                    IEvent eventObj = (IEvent)Activator.CreateInstance(type, repository);
                    eventObj.Handle(message);
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                index++;
            }
        }
    }
}
