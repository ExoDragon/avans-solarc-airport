using CustomerManagementApi.Commands;
using CustomerManagementApi.Domain;
using CustomerManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Repositories
{
    public interface ICustomerEventRepository
    {
        public CustomerAggregate GetCustomer(string id);
        public IEnumerable<CustomerAggregate> GetCustomers();
        public Task CreateCustomer(Customer customer, ICommand command);
        public Task UpdateCustomer(Customer customer, ICommand command);
    }
}
