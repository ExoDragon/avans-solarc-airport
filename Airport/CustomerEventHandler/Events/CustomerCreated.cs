using CustomerEventHandler.Domain;
using CustomerEventHandler.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEventHandler.Events
{
    public class CustomerCreated : IEvent
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCreated(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(string messageData)
        {
            Customer customer = JsonConvert.DeserializeObject<Customer>(messageData);

            await this._customerRepository.CustomerCreated(customer);
        }
    }
}
