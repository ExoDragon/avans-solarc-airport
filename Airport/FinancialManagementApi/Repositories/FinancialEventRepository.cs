using FinancialManagementApi.Commands;
using FinancialManagementApi.DBContext;
using FinancialManagementApi.Domain;
using FinancialManagementApi.Domain.Aggregates;
using FinancialManagementApi.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.Repositories
{
    public class FinancialEventRepository : IFinancialEventRepository
    {
        private readonly FinancialManagementEventDbContext _context;
        public FinancialEventRepository(FinancialManagementEventDbContext context) 
        {
            this._context = context;
        }

        public IEnumerable<InvoiceAggregate> GetInvoices()
        {
            return _context.Invoice.Include(f => f.Events);
        }


        public async Task SendInvoice(Invoice invoice, ICommand command)
        {
            InvoiceAggregate aggregate = new InvoiceAggregate()
            {
                Id = invoice.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(invoice)
                    }
                }
            };

            this._context.Invoice.Add(aggregate);
            await this._context.SaveChangesAsync();
        }

        public async Task SendMonthlyInvoices(List<Invoice> invoices, ICommand command)
        {

            foreach (Invoice invoice in invoices) 
            {
                InvoiceAggregate aggregate = new InvoiceAggregate()
                {
                    Id = invoice.Id,
                    CurrentVersion = 0,
                    Events = new List<Event>()
                    {
                        new Event()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Version = 0,
                            Timestamp = DateTime.Now,
                            MessageType = command.GetRoutingKey(),
                            EventData = JsonConvert.SerializeObject(invoice)
                        }
                    }
                };

                this._context.Invoice.Add(aggregate);
            }

            await this._context.SaveChangesAsync();
        }
    }
}
