using CustomerEventHandler.DBContext;
using CustomerEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEventHandler.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerManagementReadDbContext _context;

        public CustomerRepository(CustomerManagementReadDbContext context)
        {
            _context = context;
        }

        public async Task CustomerCreated(Customer customer)
        {
            this._context.Customer.Add(customer);
            await this._context.SaveChangesAsync();
        }

        public async Task CustomerUpdated(Customer customer)
        {
            this._context.Customer.Update(customer);
            await this._context.SaveChangesAsync();
        }

        public Customer GetById(string id)
        {
            return this._context.Customer.Find(id);
        }
    }
}
