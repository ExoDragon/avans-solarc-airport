using GateManagementApi.Commands;
using GateManagementApi.DBContext;
using GateManagementApi.Domain;
using GateManagementApi.Domain.Aggregate;
using GateManagementApi.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Repositories
{
    public class GateEventRepository : IGateEventRepository
    {

        private readonly GateManagementEventDbContext _context;

        public GateEventRepository(GateManagementEventDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GateAggregate> GetGates()
        {
            return _context.Gate.Include(g => g.Events);
        }

        public async Task GateCreated(Gate gate, ICommand command)
        {
            GateAggregate aggregate = new GateAggregate()
            {
                Id = gate.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(gate)
                    }
                }
            };

            _context.Gate.Add(aggregate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGate(Gate gate, ICommand command)
        {
            GateAggregate aggregate = _context.Gate.Include(g => g.Events).SingleOrDefault(g => g.Id == gate.Id);

            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(gate)
                });


            _context.Gate.Update(aggregate);
            await _context.SaveChangesAsync();
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

        public async Task UpdateTicket(Ticket ticket, ICommand command)
        {
            TicketAggregate aggregate = _context.Ticket.Include(g => g.Events).SingleOrDefault(g => g.Id == ticket.Id);

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

        public IEnumerable<TicketAggregate> GetTickets()
        {
            return _context.Ticket.Include(t => t.Events);
        }
    }
}
