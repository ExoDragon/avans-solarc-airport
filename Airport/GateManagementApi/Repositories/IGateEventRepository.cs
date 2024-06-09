using GateManagementApi.Commands;
using GateManagementApi.Domain;
using GateManagementApi.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Repositories
{
    public interface IGateEventRepository
    {
        public Task GateCreated(Gate gate, ICommand command);
        public Task UpdateGate(Gate gate, ICommand command);
        public Task CreateTicket(Ticket ticket, ICommand command);
        public Task UpdateTicket(Ticket ticket, ICommand command);
        public IEnumerable<GateAggregate> GetGates();
        public IEnumerable<TicketAggregate> GetTickets();
    }
}
