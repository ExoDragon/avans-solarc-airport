using Newtonsoft.Json;
using GateEventHandler.Domain;
using GateEventHandler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GateEventHandler.Events
{
    public class PassengerBoarded : IEvent
    {
        private readonly IGateRepository _gateRepository;

        public PassengerBoarded(IGateRepository gateRepository)
        {
            _gateRepository = gateRepository;
        }

        public async Task Handle(string messageData)
        {
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(messageData);
            await this._gateRepository.UpdateTicket(ticket);
        }
    }
}
