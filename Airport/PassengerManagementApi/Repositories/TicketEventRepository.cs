using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PassengerManagementApi.Commands;
using PassengerManagementApi.DBContext;
using PassengerManagementApi.Domain;
using PassengerManagementApi.Domain.Aggregates;
using PassengerManagementApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public class TicketEventRepository : ITicketEventRepository
    {
        private readonly PassengerManagementEventDbContext _context;

        public TicketEventRepository(PassengerManagementEventDbContext context)
        {
            _context = context;
        }

        public async Task CreateTicket(Ticket ticket, ICommand command)
        {
            TicketAggregate aggregate = new TicketAggregate()
            {
                Id = ticket.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(ticket)
                    }
                }
            };

            _context.Ticket.Add(aggregate);
            await _context.SaveChangesAsync();
        }

        public TicketAggregate GetById(string id)
        {
            return _context.Ticket.Include(t => t.Events).SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<TicketAggregate> GetTickets()
        {
            return _context.Ticket.Include(t => t.Events);
        }

        public async Task UpdateTicket(Ticket ticket, ICommand command)
        {
            TicketAggregate aggregate = _context.Ticket.Include(p => p.Events).SingleOrDefault(p => p.Id == ticket.Id);
            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(ticket)
                });

            _context.Ticket.Update(aggregate);
            await _context.SaveChangesAsync();
        }
    }
}
