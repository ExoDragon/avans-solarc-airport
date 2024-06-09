using FinancialEventHandler.DBContext;
using FinancialEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialEventHandler.Repositories
{
    public class FinacialRepository : IFinancialRepository
    {
        private readonly FinancialManagementReadDbContext _context;
        public FinacialRepository(FinancialManagementReadDbContext context) 
        {
            this._context = context;
        }

        public Lease GetLease(string id)
        {
            Lease lease = this._context.Lease.SingleOrDefault(f => f.Id == id);
            return lease;
        }

        public Lease GetLeaseByZoneCode(string code)
        {
            return this._context.Lease.SingleOrDefault(f => f.ZoneName == code);
        }

        public async Task AddLease(Lease lease)
        {
            this._context.Lease.Add(lease);
            await this._context.SaveChangesAsync();
        }

        public async Task EndLease(Lease lease)
        {
            this._context.Update(lease);
            await this._context.SaveChangesAsync();
        }

        public async Task SendInvoice(Invoice invoice)
        {
            this._context.Invoice.Add(invoice);
            await this._context.SaveChangesAsync();
        }

        public async Task SendMonthlyInvoices(List<Invoice> invoices)
        {
            this._context.Invoice.AddRange(invoices);
            await this._context.SaveChangesAsync();
        }

        public Zone GetZone(string code)
        {
            Zone zone = this._context.Zone.SingleOrDefault(f => f.Code == code);
            return zone;
        }

        public async Task CreateZone(Zone zone)
        {
            this._context.Zone.Add(zone);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateZone(Zone zone) 
        {
            this._context.Zone.Update(zone);
            await this._context.SaveChangesAsync();
        }

        public async Task AddCustomer(Customer customer) 
        {
            this._context.Customer.Add(customer);
            await this._context.SaveChangesAsync();
        }
        
    }
}
