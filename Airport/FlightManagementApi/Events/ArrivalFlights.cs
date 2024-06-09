using FlightManagementApi.Handlers;
using FlightManagementApi.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using FlightManagementApi.Domain;
using Newtonsoft.Json;
using FlightManagementApi.Commands;

namespace FlightManagementApi.Events
{
    public class ArrivalFlights : IEvent
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly IModel _channel;
        private readonly IArrivalFlightRepository _arrivalFlightRepository;
        private readonly IFlightEventRepository _flightEventRepository;
        private readonly IFlightMapper _flightMapper;

        public ArrivalFlights(ServiceProvider serviceProvider, IConfiguration config, IModel channel)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _channel = channel;
            _arrivalFlightRepository = _serviceProvider.GetService<IArrivalFlightRepository>(); ;
            _flightEventRepository = _serviceProvider.GetService<IFlightEventRepository>(); ;
            _flightMapper = _serviceProvider.GetService<IFlightMapper>(); ;
        }

        public async Task Handle(string messageData)
        {
            IEnumerable<Flight> flights = _flightMapper.MapFlights(await _arrivalFlightRepository.GetFlights());

            ICommand command = new FlightCommand(JsonConvert.SerializeObject(flights), "ArrivalFlightsPlanned");

            foreach (Flight flight in flights)
            {
                await _flightEventRepository.PlanFlight(flight, command);
            }

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
