using Newtonsoft.Json;
using GateEventHandler.Domain;
using GateEventHandler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateEventHandler.Events
{
    public class GateTicketCreated : IEvent
    {
        private readonly IGateRepository _gateRepository;

        public GateTicketCreated(IGateRepository gateRepository)
        {
            _gateRepository = gateRepository;
        }

        public async Task Handle(string messageData)
        {
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(messageData);
            await this._gateRepository.TicketCreated(ticket);
        }
    }
}
