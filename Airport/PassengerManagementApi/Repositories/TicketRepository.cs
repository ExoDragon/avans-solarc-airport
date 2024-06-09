using PassengerManagementApi.DBContext;
using PassengerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly PassengerManagementReadDbContext _context;

        public TicketRepository(PassengerManagementReadDbContext context)
        {
            _context = context;
        }

        public Ticket GetById(string id)
        {
            return _context.Ticket.SingleOrDefault(t => t.Id == id);
        }

        public Ticket GetByTicketCode(string code)
        {
            return _context.Ticket.SingleOrDefault(t => t.Code == code);
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return _context.Ticket;
        }

        public IEnumerable<Ticket> GetTicketsByFlightId(string id)
        {
            return _context.Ticket.Where(t => t.FlightId == id);
        }

        public IEnumerable<Ticket> GetTicketsByPassengerId(string id)
        {
            return _context.Ticket.Where(t => t.PassengerId == id);
        }
    }
}
