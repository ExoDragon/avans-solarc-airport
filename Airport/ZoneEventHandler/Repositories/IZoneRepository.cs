using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZoneEventHandler.Domain;

namespace ZoneEventHandler.Repositories
{
    public interface IZoneRepository
    {
        public Lease GetLease(string id);
        public Zone GetZone(string code);
        public Task UpdateZone(Zone zone);
        public Task ZoneCreated(Zone zone);
        public Task CustomerCreated(Customer customer);
        public Customer GetCustomerById(string id);
        public Task UpdateCustomer(Customer customer);
        public Task CustomerLeaseStarted(Lease lease);
        public Task CustomerLeaseEnded(Lease lease);
    }
}
