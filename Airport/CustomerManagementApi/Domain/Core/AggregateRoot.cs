using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Domain.Core
{
    public class AggregateRoot
    {
        public string Id { get; set; }
        public int CurrentVersion { get; set; }
        public List<Event> Events { get; set; }
    }
}
