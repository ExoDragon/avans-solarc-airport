using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GateEventHandler.Domain;

namespace GateEventHandler.Repositories
{
    public interface IGateRepository
    {
        public Gate GetGate(string id);
        public Task GateCreated(Gate gate);
        public Task UpdateGate(Gate gate);
        public Ticket GetTicket(string id);
        public Task TicketCreated(Ticket ticket);
        public Task UpdateTicket(Ticket ticket);
    }
}
