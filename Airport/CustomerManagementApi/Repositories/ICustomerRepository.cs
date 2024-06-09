using CustomerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Repositories
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetCustomers();
        public Customer GetById(string id);
    }
}
