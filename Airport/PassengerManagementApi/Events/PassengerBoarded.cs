using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PassengerManagementApi.Commands;
using PassengerManagementApi.Domain;
using PassengerManagementApi.Handlers;
using PassengerManagementApi.Repositories;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerManagementApi.Events
{
    public class PassengerBoarded : IEvent
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly IModel _channel;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEventRepository _ticketEventRepository;

        public PassengerBoarded(ServiceProvider serviceProvider, IConfiguration config, IModel channel)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _channel = channel;
            _ticketRepository = serviceProvider.GetService<ITicketRepository>();
            _ticketEventRepository = serviceProvider.GetService<ITicketEventRepository>();
        }

        public async Task Handle(string messageData)
        {
            try
            {
                Ticket ticket = _ticketRepository.GetById(messageData);
                ticket.Status = "Boarded";
                ICommand command = new TicketCommand(JsonConvert.SerializeObject(ticket), "TicketUpdated");
                await _ticketEventRepository.UpdateTicket(ticket, command);

                _channel.ExchangeDeclare(exchange: _config["RabbitMQHandler:Exchange"], type: "topic");

                var routingKey = command.GetRoutingKey();
                var message = command.GetMessage();
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: _config["RabbitMQHandler:Exchange"],
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                int tickets = _ticketRepository.GetTicketsByFlightId(ticket.FlightId).Where(t => t.Status != "Boarded").Count();

                if(tickets == 0)
                {
                    var obj = new { Id = ticket.FlightId, GateCode = ticket.GateCode };
                    ICommand boardCommand = new TicketCommand(JsonConvert.SerializeObject(obj), "AllPassengersBoarded");
                    _channel.ExchangeDeclare(exchange: _config["RabbitMQHandler:Exchange"], type: "topic");

                    var routingKeyBoard = boardCommand.GetRoutingKey();
                    var messageBoard = boardCommand.GetMessage();
                    var bodyBoard = Encoding.UTF8.GetBytes(messageBoard);
                    _channel.BasicPublish(exchange: _config["RabbitMQHandler:Exchange"],
                                         routingKey: routingKeyBoard,
                                         basicProperties: null,
                                         body: bodyBoard);

                    Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
