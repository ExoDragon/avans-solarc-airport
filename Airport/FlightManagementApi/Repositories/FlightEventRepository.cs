using FlightManagementApi.Commands;
using FlightManagementApi.DBContext;
using FlightManagementApi.Domain;
using FlightManagementApi.Domain.Aggregates;
using FlightManagementApi.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Repositories
{
    public class FlightEventRepository : IFlightEventRepository
    {
        private readonly FlightManagementEventDbContext _context;
        public FlightEventRepository(FlightManagementEventDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FlightAggregate> GetFlights()
        {
            return _context.Flight.Include(f => f.Events);
        }

        public async Task PlanFlight(Flight flight, ICommand command)
        {

            FlightAggregate aggregate = new FlightAggregate()
            {
                Id = flight.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(flight)
                    }
                }
            };

            _context.Flight.Add(aggregate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlight(Flight flight, ICommand command)
        {
            FlightAggregate aggregate = _context.Flight.Include(f => f.Events).SingleOrDefault(f => f.Id == flight.Id);

            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(flight)
                });

            _context.Flight.Update(aggregate);
            await _context.SaveChangesAsync();
        }
    }
}
