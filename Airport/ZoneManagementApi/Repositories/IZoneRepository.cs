using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZoneManagementApi.Domain;

namespace ZoneManagementApi.Repositories
{
    public interface IZoneRepository
    {
        public IEnumerable<Zone> GetZones();
        public IEnumerable<Lease> GetLeases();
        public IEnumerable<Customer> GetCustomers();

        public Zone GetZoneById(string id);
        public Lease GetLeaseById(string id);
        public Customer GetCustomerById(string id);

    }
}
