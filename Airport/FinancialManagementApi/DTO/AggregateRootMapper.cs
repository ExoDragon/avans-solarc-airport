using FinancialManagementApi.Domain;
using FinancialManagementApi.Domain.Aggregates;
using FinancialManagementApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO
{
    public static class AggregateRootMapper
    {
        public static IEnumerable<InvoiceAggregateRoot> Map(this IEnumerable<Invoice> invoices, IEnumerable<InvoiceAggregate> invoiceAggregates)
        {
            List<InvoiceAggregateRoot> aggregateRoot = new List<InvoiceAggregateRoot>();

            foreach (Invoice invoice in invoices)
            {
                InvoiceAggregate aggregate = invoiceAggregates.SingleOrDefault(f => f.Id == invoice.Id);

                aggregateRoot.Add(new InvoiceAggregateRoot(
                        invoice.Id, 
                        invoice.CustomerName,
                        invoice.CustomerEmail,
                        invoice.ZoneName,
                        invoice.ZoneRentalPrice,
                        invoice.BillingDate,
                        aggregate.Events, 
                        aggregate.CurrentVersion
                    )
                );
            }

            return aggregateRoot;
        }
    }
}
