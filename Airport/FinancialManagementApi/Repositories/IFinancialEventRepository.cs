using FinancialManagementApi.Commands;
using FinancialManagementApi.Domain;
using FinancialManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.Repositories
{
    public interface IFinancialEventRepository
    {
        public Task SendInvoice(Invoice invoice, ICommand command);

        public Task SendMonthlyInvoices(List<Invoice> invoices, ICommand command);

        public IEnumerable<InvoiceAggregate> GetInvoices();
    }
}
