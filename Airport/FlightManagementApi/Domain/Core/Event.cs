using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Domain.Core
{
    public class Event
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public DateTime Timestamp { get; set; }
        public string MessageType { get; set; }
        [Column(TypeName = "text")]
        public string EventData { get; set; }
    }
}
