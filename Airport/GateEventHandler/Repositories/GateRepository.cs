using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GateEventHandler.Domain;
using GateEventHandler.DBContext;

namespace GateEventHandler.Repositories
{
    public class GateRepository : IGateRepository
    {
        private readonly GateManagementReadDbContext _context;
        public GateRepository(GateManagementReadDbContext context)
        {
            _context = context;
        }

        public async Task GateCreated(Gate gate)
        {
            this._context.Gate.Add(gate);
            await this._context.SaveChangesAsync();
        }

        public Gate GetGate(string id)
        {
            Gate gate = this._context.Gate.SingleOrDefault(g => g.Id == id);
            return gate;
        }

        public async Task UpdateGate(Gate gate)
        {
            this._context.Gate.Update(gate);
            await this._context.SaveChangesAsync();
        }

        public Ticket GetTicket(string id)
        {
            Ticket ticket = this._context.Ticket.SingleOrDefault(t => t.Id == id);
            return ticket;
        }

        public async Task TicketCreated(Ticket ticket)
        {
            this._context.Ticket.Add(ticket);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            this._context.Ticket.Update(ticket);
            await this._context.SaveChangesAsync();
        }
    }
}
