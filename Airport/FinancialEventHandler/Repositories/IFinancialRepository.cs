using FinancialEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialEventHandler.Repositories
{
    public interface IFinancialRepository
    {
        public Lease GetLease(string id);
        public Lease GetLeaseByZoneCode(string code);
        public Task AddLease(Lease lease);
        public Task EndLease(Lease lease);
        public Zone GetZone(string code);
        public Task CreateZone(Zone zone);
        public Task UpdateZone(Zone zone);
        public Task SendInvoice(Invoice invoice);
        public Task SendMonthlyInvoices(List<Invoice> invoices);
        public Task AddCustomer(Customer customer);
    }
}
