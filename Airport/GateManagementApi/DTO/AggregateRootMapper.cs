using GateManagementApi.Domain;
using GateManagementApi.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.DTO
{
    public static class AggregateRootMapper
    {
        public static IEnumerable<GateAggregateRoot> Map(this IEnumerable<Gate> zones, IEnumerable<GateAggregate> zoneAggregates)
        {
            List<GateAggregateRoot> aggregateRoot = new List<GateAggregateRoot>();

            foreach (Gate gate in zones)
            {
                GateAggregate aggregate = zoneAggregates.SingleOrDefault(g => g.Id == gate.Id);

                aggregateRoot.Add(new GateAggregateRoot(
                        gate.Id,
                        gate.Name,
                        gate.Status,
                        aggregate.Events,
                        aggregate.CurrentVersion
                    )
                );
            }

            return aggregateRoot;
        }
    }
}
