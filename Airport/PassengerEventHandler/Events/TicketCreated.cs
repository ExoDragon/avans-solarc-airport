using Newtonsoft.Json;
using PassengerEventHandler.Domain;
using PassengerEventHandler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerEventHandler.Events
{
    public class TicketCreated : IEvent
    {
        private readonly IPassengerRepository _passengerRepository;

        public TicketCreated(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task Handle(string messageData)
        {
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(messageData);
            await this._passengerRepository.CreateTicket(ticket);
        }
    }
}
