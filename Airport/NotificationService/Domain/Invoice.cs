using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain
{
    public class Invoice
    {

        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ZoneName { get; set; }
        public float ZoneRentalPrice { get; set; }
        public DateTime BillingDate { get; set; }

        public Invoice(string id, string customerName, string customerEmail, string zoneName, float zoneRentalPrice, DateTime billingDate)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.CustomerEmail = customerEmail;
            this.ZoneName = zoneName;
            this.ZoneRentalPrice = zoneRentalPrice;
            this.BillingDate = billingDate;
        }
    }
}
