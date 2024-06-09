using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZoneEventHandler.Domain;
using ZoneEventHandler.DBContext;

namespace ZoneEventHandler.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly ZoneManagementReadDbContext _context;
        public ZoneRepository(ZoneManagementReadDbContext context)
        {
            _context = context;
        }

        public async Task CustomerCreated(Customer customer)
        {
            this._context.Customer.Add(customer);
            await this._context.SaveChangesAsync();
        }

        public async Task CustomerLeaseEnded(Lease lease)
        {
            lease.Status = "Ended";
            this._context.Lease.Update(lease);
            await this._context.SaveChangesAsync();
        }

        public async Task CustomerLeaseStarted(Lease lease)
        {

            this._context.Lease.Add(lease);
            await this._context.SaveChangesAsync();
        }

        public Customer GetCustomerById(string id)
        {
            return this._context.Customer.Find(id);
        }

        public Lease GetLease(string id)
        {
            Lease lease = this._context.Lease.SingleOrDefault(l => l.Id == id);
            return lease;
        }

        public Zone GetZone(string code)
        {
            Zone zone = this._context.Zone.SingleOrDefault(z => z.Code == code);
            return zone;
        }

        public async Task UpdateCustomer(Customer customer)
        {
            this._context.Customer.Update(customer);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateZone(Zone zone)
        {
            this._context.Zone.Update(zone);
            await this._context.SaveChangesAsync();
        }

        public async Task ZoneCreated(Zone zone)
        {
            this._context.Zone.Add(zone);
            await this._context.SaveChangesAsync();
        }
    }
}
