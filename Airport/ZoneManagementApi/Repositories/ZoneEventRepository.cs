using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using ZoneManagementApi.DBContext;
using ZoneManagementApi.Commands;
using ZoneManagementApi.Domain;
using ZoneManagementApi.Domain.Aggregate;
using ZoneManagementApi.Domain.Core;

namespace ZoneManagementApi.Repositories
{
    public class ZoneEventRepository : IZoneEventRepository
    {

        private readonly ZoneManagementEventDbContext _context;
        public ZoneEventRepository(ZoneManagementEventDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ZoneAggregate> GetZones()
        {
            return _context.Zone.Include(z => z.Events);
        }

        public IEnumerable<LeaseAggregate> GetLeases()
        {
            return _context.Lease.Include(l => l.Events);
        }

        public async Task ZoneCreated(Zone zone, ICommand command)
        {
            ZoneAggregate aggregate = new ZoneAggregate()
            {
                Id = zone.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(zone)
                    }
                }
            };

            _context.Zone.Add(aggregate);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateZone(Zone zone, ICommand command)
        {
            ZoneAggregate aggregate = _context.Zone.Include(z => z.Events).SingleOrDefault(z => z.Id == zone.Id);

            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(zone)
                });

            _context.Zone.Update(aggregate);
            await _context.SaveChangesAsync();
        }

        public async Task LeaseZone(Lease lease, ICommand command)
        {
            LeaseAggregate aggregate = new LeaseAggregate()
            {
                Id = lease.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(lease)
                    }
                }
            };

            _context.Lease.Add(aggregate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLease(Lease lease, ICommand command)
        {
            LeaseAggregate aggregate = _context.Lease.Include(l => l.Events).SingleOrDefault(l => l.Id == lease.Id);

            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(lease)
                });

            _context.Lease.Update(aggregate);
            await _context.SaveChangesAsync();
        }
    }
}
