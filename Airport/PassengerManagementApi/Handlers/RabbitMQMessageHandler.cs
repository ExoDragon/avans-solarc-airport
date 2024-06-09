using PassengerManagementApi.Commands;
using PassengerManagementApi.Events;
using PassengerManagementApi.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerManagementApi.Handlers
{
    public class RabbitMQMessageHandler : IMessageHandler
    {
        private readonly IModel _channel;
        private readonly IConfiguration _config;

        public RabbitMQMessageHandler(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            _config = serviceProvider.GetService<IConfiguration>();
            var factory = new ConnectionFactory() { HostName = _config["RabbitMQHandler:Host"], UserName = _config["RabbitMQHandler:Username"], Password = _config["RabbitMQHandler:Password"] };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _config["RabbitMQHandler:Exchange"], type: "topic");
            var queueName = _channel.QueueDeclare(
                                queue: _config["RabbitMQHandler:Queue"],
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null
                            ).QueueName;

            int index = 0;
            while (_config[$"RabbitMQHandler:RoutingKeys:{index}:RoutingKey"] != null)
            {
                _channel.QueueBind(queue: queueName, exchange: _config["RabbitMQHandler:Exchange"], routingKey: _config[$"RabbitMQHandler:RoutingKeys:{index}:RoutingKey"]);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);

                    var type = Type.GetType($"PassengerManagementApi.Events.{routingKey}");
                    IEvent eventObj = (IEvent)Activator.CreateInstance(type, serviceProvider, _config, _channel);
                    eventObj.Handle(message);
                };

                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                index++;
            }
        }

        public void Publish(ICommand command)
        {
            _channel.ExchangeDeclare(exchange: _config["RabbitMQHandler:Exchange"], type: "topic");

            var routingKey = command.GetRoutingKey();
            var message = command.GetMessage();
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _config["RabbitMQHandler:Exchange"],
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
        }
    }
}
