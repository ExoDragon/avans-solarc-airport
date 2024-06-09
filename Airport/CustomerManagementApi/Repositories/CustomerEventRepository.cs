using CustomerManagementApi.Commands;
using CustomerManagementApi.DBContext;
using CustomerManagementApi.Domain;
using CustomerManagementApi.Domain.Aggregates;
using CustomerManagementApi.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Repositories
{
    public class CustomerEventRepository : ICustomerEventRepository
    {
        private readonly CustomerManagementEventDbContext _context;

        public CustomerEventRepository(CustomerManagementEventDbContext context)
        {
            _context = context;
        }

        public CustomerAggregate GetCustomer(string id)
        {
            return _context.Customer.Include(c => c.Events).SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<CustomerAggregate> GetCustomers()
        {
            return _context.Customer.Include(c => c.Events);
        }

        public async Task CreateCustomer(Customer customer, ICommand command)
        {
            CustomerAggregate aggregate = new CustomerAggregate()
            {
                Id = customer.Id,
                CurrentVersion = 0,
                Events = new List<Event>()
                {
                    new Event()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Version = 0,
                        Timestamp = DateTime.Now,
                        MessageType = command.GetRoutingKey(),
                        EventData = JsonConvert.SerializeObject(customer)
                    }
                }
            };

            _context.Customer.Add(aggregate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer, ICommand command)
        {
            CustomerAggregate aggregate = _context.Customer.Include(c => c.Events).SingleOrDefault(c => c.Id == customer.Id);
            aggregate.CurrentVersion = aggregate.Events.Count;

            aggregate.Events.Add(
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = aggregate.Events.Count,
                    Timestamp = DateTime.Now,
                    MessageType = command.GetRoutingKey(),
                    EventData = JsonConvert.SerializeObject(customer)
                });

            _context.Customer.Update(aggregate);
            await _context.SaveChangesAsync();
        }
    }
}
