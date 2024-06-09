using PassengerManagementApi.Commands;
using PassengerManagementApi.Domain;
using PassengerManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public interface ITicketEventRepository
    {
        public TicketAggregate GetById(string id);
        public IEnumerable<TicketAggregate> GetTickets();
        public Task CreateTicket(Ticket ticket, ICommand command);
        public Task UpdateTicket(Ticket ticket, ICommand command);
    }
}
