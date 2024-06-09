using PassengerEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerEventHandler.Repositories
{
    public interface IPassengerRepository
    {
        public Passenger GetById(string id);
        public Task CreatePassenger(Passenger passenger);
        public Task UpdatePassenger(Passenger passenger);

        public Ticket GetTicketById(string id);
        public Task CreateTicket(Ticket ticket);
        public Task UpdateTicket(Ticket ticket);
    }
}
