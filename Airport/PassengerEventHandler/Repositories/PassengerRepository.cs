using PassengerEventHandler.DBContext;
using PassengerEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerEventHandler.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly PassengerManagementReadDbContext _context;

        public PassengerRepository(PassengerManagementReadDbContext context)
        {
            _context = context;
        }

        public async Task CreatePassenger(Passenger passenger)
        {
            this._context.Passenger.Add(passenger);
            await this._context.SaveChangesAsync();
        }

        public Passenger GetById(string id)
        {
            return this._context.Passenger.SingleOrDefault(p => p.Id == id);
        }

        public async Task UpdatePassenger(Passenger passenger)
        {
            this._context.Passenger.Update(passenger);
            await this._context.SaveChangesAsync();
        }

        public async Task CreateTicket(Ticket ticket)
        {
            this._context.Ticket.Add(ticket);
            await this._context.SaveChangesAsync();
        }

        public Ticket GetTicketById(string id)
        {
            return this._context.Ticket.SingleOrDefault(t => t.Id == id);
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            this._context.Ticket.Update(ticket);
            await this._context.SaveChangesAsync();
        }
    }
}
