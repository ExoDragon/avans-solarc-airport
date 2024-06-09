using GateManagementApi.Domain;
using GateManagementApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.DTO
{
    public class GateAggregateRoot : Gate
    {
        public IList<Event> Events { get; set; }
        public int CurrentVersion { get; set; }

        public GateAggregateRoot(
            string id,
            string name,
            string status,
            IList<Event> events,
            int version
        ) : base (
                id,
                name,
                status
                )
        {
            this.Events = events;
            this.CurrentVersion = version;
        }
    }
}
