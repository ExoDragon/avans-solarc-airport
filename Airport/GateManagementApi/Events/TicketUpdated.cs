using Newtonsoft.Json;
using GateManagementApi.Domain;
using GateManagementApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GateManagementApi.Commands;
using GateManagementApi.Handlers;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;

namespace GateManagementApi.Events
{
    public class TicketUpdated : IEvent
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly IModel _channel;
        private readonly IGateEventRepository _gateEventRepository;
        private readonly IGateRepository _gateRepository;

        public TicketUpdated(ServiceProvider serviceProvider, IConfiguration config, IModel channel)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _channel = channel;
            _gateEventRepository = serviceProvider.GetService<IGateEventRepository>();
            _gateRepository = serviceProvider.GetService<IGateRepository>();
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            var id = jObject["Id"].ToString();
            var code = jObject["Code"].ToString();
            var gateCode = jObject["GateCode"].ToString();
            string gateId = _gateRepository.GetGateByCode(gateCode).Id;
            var status = jObject["Status"].ToString();
            var passengerId = jObject["PassengerId"].ToString();
            var passengerName = jObject["PassengerName"].ToString();
            var passengerEmail = jObject["PassengerEmail"].ToString();
            var passengerPhonenumber = jObject["PassengerPhonenumber"].ToString();

            Ticket ticket = new Ticket()
            {
                Id = id,
                Code = code,
                GateId = gateId,
                GateCode = gateCode,
                Status = status,
                PassengerId = passengerId,
                PassengerName = passengerName,
                PassengerEmail = passengerEmail,
                PassengerPhonenumber = passengerPhonenumber
            };

            ICommand command = new TicketCommand(JsonConvert.SerializeObject(ticket), "GateTicketUpdated");

            await _gateEventRepository.UpdateTicket(ticket, command);

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
