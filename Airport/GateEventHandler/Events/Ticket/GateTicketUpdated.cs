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
    public class GateTicketUpdated : IEvent
    {
        private readonly IGateRepository _gateRepository;

        public GateTicketUpdated(IGateRepository gateRepository)
        {
            _gateRepository = gateRepository;
        }

        public async Task Handle(string messageData)
        {
            Ticket Incommingticket = JsonConvert.DeserializeObject<Ticket>(messageData);
            Ticket ticket = this._gateRepository.GetTicket(Incommingticket.Id);

            ticket.Code = Incommingticket.Code;
            ticket.GateId = Incommingticket.GateId;
            ticket.GateCode = Incommingticket.GateCode;
            ticket.Status = Incommingticket.Status;
            ticket.PassengerId = Incommingticket.PassengerId;
            ticket.PassengerEmail = Incommingticket.PassengerEmail;
            ticket.PassengerPhonenumber = Incommingticket.PassengerPhonenumber;

            await this._gateRepository.UpdateTicket(ticket);
        }
    }
}
