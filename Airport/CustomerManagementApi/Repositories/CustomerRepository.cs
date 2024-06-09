using CustomerManagementApi.DBContext;
using CustomerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerManagementReadDbContext _context;

        public CustomerRepository(CustomerManagementReadDbContext context)
        {
            _context = context;
        }

        public Customer GetById(string id)
        {
            return _context.Customer.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customer;
        }
    }
}
