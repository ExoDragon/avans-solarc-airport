using GateManagementApi.Commands;
using GateManagementApi.Domain;
using GateManagementApi.Events;
using GateManagementApi.Repositories;
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
        private readonly IGateEventRepository _gateEventRepository;
        private readonly IGateRepository _gateRepository;
        private readonly IConfiguration _config;
        private readonly IModel _channel;

        public AllPassengersBoarded(ServiceProvider serviceProvider, IConfiguration config, IModel channel)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _channel = channel;
            _gateEventRepository = serviceProvider.GetService<IGateEventRepository>();
            _gateRepository = serviceProvider.GetService<IGateRepository>();
        }

        public async Task Handle(string messageData)
        {
            try
            {
                JObject jObject = JObject.Parse(messageData);

                Gate gate = _gateRepository.GetGateByCode(jObject["GateCode"].ToString());
                gate.Status = "Closed";

                ICommand command = new GateCommand(JsonConvert.SerializeObject(gate), "GateClosed");
                await _gateEventRepository.UpdateGate(gate, command);

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
