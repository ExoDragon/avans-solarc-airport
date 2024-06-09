using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Domain.Core
{
    public abstract class AggregateRoot
    {
        public string Id { get; set; }
        public int CurrentVersion { get; set; }
        public List<Event> Events { get; set; }
    }
}
