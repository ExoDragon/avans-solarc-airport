using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace Airport
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "rabbitmquser", Password = "DEBmbwkSrzy9D1T9cJfa" };
            var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "Airport", type: "topic");

                var routingKey = (args.Length > 0) ? args[0] : "FlightPlanned";
                var message = (args.Length > 1)
                              ? string.Join(" ", args.Skip(1).ToArray())
                              : "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "Airport",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
            }
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "Airport", type: "topic");

                var routingKey = (args.Length > 0) ? args[0] : "FlightCancelled";
                var message = (args.Length > 1)
                              ? string.Join(" ", args.Skip(1).ToArray())
                              : "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "Airport",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
            }
        }
    }
}
