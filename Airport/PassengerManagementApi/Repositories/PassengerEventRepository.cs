using Microsoft.EntityFrameworkCore;
using PassengerManagementApi.DBContext;
using PassengerManagementApi.Domain;
using PassengerManagementApi.Domain.Core;
using PassengerManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassengerManagementApi.Commands;
using Newtonsoft.Json;

namespace PassengerManagementApi.Repositories
{
    public class PassengerEventRepository : IPassengerEventRepository
    {
        private readonly PassengerManagementEventDbContext _context;

        public PassengerEventRepository(PassengerManagementEventDbContext context)
        {
            _context = context;
        }

        public async Task CreatePassenger(Passenger passenger, ICommand command)
        {
            PassengerAggregate aggregate = new PassengerAggregate()
            {
                Id = passenger.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(passenger)
                    }
                }
            };

            _context.Passenger.Add(aggregate);
            await _context.SaveChangesAsync();
        }

        public PassengerAggregate GetById(string id)
        {
            return _context.Passenger.Include(p => p.Events).SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<PassengerAggregate> GetPassengers()
        {
            return _context.Passenger.Include(p => p.Events);
        }

        public async Task UpdatePassenger(Passenger passenger, ICommand command)
        {
            PassengerAggregate aggregate = _context.Passenger.Include(p => p.Events).SingleOrDefault(p => p.Id == passenger.Id);
            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(passenger)
                });

            _context.Passenger.Update(aggregate);
            await _context.SaveChangesAsync();
        }
    }
}
