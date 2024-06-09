using GateManagementApi.DBContext;
using GateManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Repositories
{
    public class GateRepository : IGateRepository
    {
        private readonly GateManagementReadDbContext _context;
        public GateRepository(GateManagementReadDbContext context)
        {
            _context = context;
        }

        public Gate GetGateById(string id)
        {
            return _context.Gate.SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gate> GetGates()
        {
            return _context.Gate;
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return _context.Ticket;
        }

        public Ticket GetTicketById(string id)
        {
            return _context.Ticket.SingleOrDefault(t => t.Id == id);
        }

        public Gate GetGateByCode(string code)
        {
            return _context.Gate.SingleOrDefault(g => g.Name == code);
        }
    }
}
