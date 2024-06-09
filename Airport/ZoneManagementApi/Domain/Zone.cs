using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneManagementApi.Domain
{
    public class Zone
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public Zone(string id, string code, string status)
        {
            Id = id;
            Code = code;
            Status = status;
        }

        public Zone(string code, string status)
        {
            Code = code;
            Status = status;
        }

        public Zone() {}
    }
}
