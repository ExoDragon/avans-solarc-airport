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
    public class CustomerUpdated : IEvent
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerUpdated(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(string messageData)
        {
            Customer Incommingcustomer = JsonConvert.DeserializeObject<Customer>(messageData);
            Customer customer = this._customerRepository.GetById(Incommingcustomer.Id);

            customer.Name = Incommingcustomer.Name;
            customer.Email = Incommingcustomer.Email;
            customer.Address = Incommingcustomer.Address;
            customer.Phonenumber = Incommingcustomer.Phonenumber;

            await this._customerRepository.CustomerUpdated(customer);
        }
    }
}
