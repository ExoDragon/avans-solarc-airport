using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneEventHandler.Domain
{
    public class Lease
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ZoneName { get; set; } 
        public float Price { get; set; }
        public string Status { get; set; }


        public Lease(string id, string customerName, string customerEmail, string zoneName, float price, string status)
        {
            Id = id;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            ZoneName = zoneName;
            Price = price;
            Status = status;
        }

        public Lease() { }
    }
}
