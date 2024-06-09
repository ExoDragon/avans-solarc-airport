using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Events;

namespace NotificationService
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
                     
            channel.QueueBind(queue: queueName, exchange: config["RabbitMQHandler:Exchange"], routingKey: "*");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;

                var type = Type.GetType($"NotificationService.Events.{routingKey}");

                if (type != null)
                {
                    IEvent eventObj = (IEvent)Activator.CreateInstance(type, serviceProvider);
                    eventObj.Handle(message);
                }

                var securityType = Type.GetType($"NotificationService.Events.SecurityEvent");
                ISecEvent secEventObj = (ISecEvent)Activator.CreateInstance(securityType);
                secEventObj.Handle(routingKey, message);
                 
                Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

          
        }
    }
}
