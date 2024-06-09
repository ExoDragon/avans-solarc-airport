using FinancialManagementApi.Domain;
using FinancialManagementApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagementApi.DTO
{
    public class InvoiceAggregateRoot : Invoice
    {
        public IList<Event> Events { get; set; }
        public int CurrentVersion { get; set; }

        public InvoiceAggregateRoot(
            string id, 
            string customerName, 
            string customerEmail, 
            string zoneName, float 
            zoneRentalPrice, 
            DateTime billingDate,
            IList<Event> events,
            int version
        ) : base(
            id, 
            customerName,
            customerEmail, 
            zoneName, 
            zoneRentalPrice,
            billingDate
        )
        {
            this.Events = events;
            this.CurrentVersion = version;
        }
    }
}
