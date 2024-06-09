using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoneManagementApi.Domain;
using ZoneManagementApi.Domain.Core;

namespace ZoneManagementApi.DTO
{
    public class LeaseAggregateRoot : Lease
    {
        public IList<Event> Events { get; set; }
        public int CurrentVersion { get; set; }

        public LeaseAggregateRoot(
            string id,
            string customername,
            string customerEmail,
            string zoneName,
            float price,
            string status,
            IList<Event> events,
            int version
        ) : base(
            id,
            customername,
            customerEmail,
            zoneName,
            price,
            status
            )
        {
            this.Events = events;
            this.CurrentVersion = version;
        }
    }
}
