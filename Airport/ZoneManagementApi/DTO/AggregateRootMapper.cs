using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZoneManagementApi.Domain;
using ZoneManagementApi.Domain.Aggregate;

namespace ZoneManagementApi.DTO
{
    public static class AggregateRootMapper
    {
        public static IEnumerable<ZoneAggregateRoot> Map(this IEnumerable<Zone> zones, IEnumerable<ZoneAggregate> zoneAggregates)
        {
            List<ZoneAggregateRoot> aggregateRoot = new List<ZoneAggregateRoot>();

            foreach (Zone zone in zones)
            {
                ZoneAggregate aggregate = zoneAggregates.SingleOrDefault(z => z.Id == zone.Id);

                aggregateRoot.Add(new ZoneAggregateRoot(
                        zone.Id,
                        zone.Code,
                        zone.Status,
                        aggregate.Events,
                        aggregate.CurrentVersion
                    )
                );
            }

            return aggregateRoot;
        }

        public static IEnumerable<LeaseAggregateRoot> Map(this IEnumerable<Lease> leases, IEnumerable<LeaseAggregate> leaseAggregates)
        {
            List<LeaseAggregateRoot> aggregateRoot = new List<LeaseAggregateRoot>();

            foreach (Lease lease in leases)
            {
                LeaseAggregate aggregate = leaseAggregates.SingleOrDefault(l => l.Id == lease.Id);

                aggregateRoot.Add(new LeaseAggregateRoot(
                        lease.Id,
                        lease.CustomerName,
                        lease.CustomerEmail,
                        lease.ZoneName,
                        lease.Price,
                        lease.Status,
                        aggregate.Events,
                        aggregate.CurrentVersion
                    )
                );
            }

            return aggregateRoot;
        }

    }
}
