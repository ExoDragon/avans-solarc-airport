using FlightManagementApi.Commands;
using FlightManagementApi.Domain;
using FlightManagementApi.Handlers;
using FlightManagementApi.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementApi.Events
{
    public class AllPassengersBoarded : IEvent
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IFlightEventRepository _flightEventRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IConfiguration _config;
        private readonly IModel _channel;

        public AllPassengersBoarded(ServiceProvider serviceProvider, IConfiguration config, IModel channel)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _channel = channel;
            _flightEventRepository = serviceProvider.GetService<IFlightEventRepository>();
            _flightRepository = serviceProvider.GetService<IFlightRepository>();
        }

        public async Task Handle(string messageData)
        {
            try
            {
                JObject jObject = JObject.Parse(messageData);

                Flight flight = _flightRepository.GetById(jObject["Id"].ToString());
                flight.Status = "Departed";

                ICommand command = new FlightCommand(JsonConvert.SerializeObject(flight), "FlightDeparted");

                await _flightEventRepository.UpdateFlight(flight, command);

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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
