using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZoneManagementApi.Commands;
using ZoneManagementApi.Domain;
using ZoneManagementApi.Domain.Aggregate;

namespace ZoneManagementApi.Repositories
{
    public interface IZoneEventRepository
    {
        public Task ZoneCreated(Zone zone, ICommand command);
        public Task UpdateZone(Zone zone, ICommand command);
        public Task LeaseZone(Lease lease, ICommand command);
        public Task UpdateLease(Lease lease, ICommand command);
        public IEnumerable<ZoneAggregate> GetZones();
        public IEnumerable<LeaseAggregate> GetLeases();
    }
}
