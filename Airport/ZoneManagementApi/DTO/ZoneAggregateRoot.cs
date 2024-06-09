using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZoneManagementApi.Domain;
using ZoneManagementApi.Domain.Core;

namespace ZoneManagementApi.DTO
{
    public class ZoneAggregateRoot : Zone
    {
        public IList<Event> Events { get; set; }
        public int CurrentVersion { get; set; }

        public ZoneAggregateRoot(
            string id,
            string code,
            string status,
            IList<Event> events,
            int version
        ) : base(
            id,
            code,
            status
            )
        {
            this.Events = events;
            this.CurrentVersion = version;
        }
    }
}
