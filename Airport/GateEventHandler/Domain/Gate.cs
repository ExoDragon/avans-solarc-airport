using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateEventHandler.Domain
{
    public class Gate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public Gate(string id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public Gate() { }
    }
}
