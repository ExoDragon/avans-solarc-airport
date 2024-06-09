using PassengerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public interface ITicketRepository
    {
        public Ticket GetById(string id);
        public Ticket GetByTicketCode(string code);
        public IEnumerable<Ticket> GetTickets();
        public IEnumerable<Ticket> GetTicketsByFlightId(string id);
        public IEnumerable<Ticket> GetTicketsByPassengerId(string id);
        
    }
}
