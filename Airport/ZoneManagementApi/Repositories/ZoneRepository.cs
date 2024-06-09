using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZoneManagementApi.DBContext;
using ZoneManagementApi.Domain;

namespace ZoneManagementApi.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly ZoneManagementReadDbContext _context;
        public ZoneRepository(ZoneManagementReadDbContext context)
        {
            _context = context;
        }

        public Zone GetZoneById(string id)
        {
            return _context.Zone.SingleOrDefault(z => z.Id == id);
        }

        public Lease GetLeaseById(string id)
        {
            return _context.Lease.SingleOrDefault(l => l.Id == id);
        }

        public Customer GetCustomerById(string id)
        {
            return _context.Customer.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Zone> GetZones()
        {
            return _context.Zone;
        }

        public IEnumerable<Lease> GetLeases()
        {
            return _context.Lease;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customer;
        }
    }
}
