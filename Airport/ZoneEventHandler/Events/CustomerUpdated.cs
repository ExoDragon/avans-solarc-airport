using ZoneEventHandler.Domain;
using ZoneEventHandler.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneEventHandler.Events
{
    public class CustomerUpdated : IEvent
    {
        private readonly IZoneRepository _zoneRepository;

        public CustomerUpdated(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            Customer customer = this._zoneRepository.GetCustomerById(jObject["Id"].ToString());
            customer.Name = jObject["Name"].ToString();
            customer.Email = jObject["Email"].ToString();

            await this._zoneRepository.UpdateCustomer(customer);
        }
    }
}
