using CustomerEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEventHandler.Repositories
{
    public interface ICustomerRepository
    {
        public Customer GetById(string id);
        public Task CustomerCreated(Customer customer);
        public Task CustomerUpdated(Customer customer);
    }
}
