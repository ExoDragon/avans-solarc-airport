using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using ZoneEventHandler.Domain;
using ZoneEventHandler.Repositories;

namespace ZoneEventHandler.Events
{
    public class CustomerCreated : IEvent
    {
        private readonly IZoneRepository _zoneRepository;

        public CustomerCreated(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            Customer customer = new Customer(
                jObject["Id"].ToString(),
                jObject["Name"].ToString(),
                jObject["Email"].ToString()
            ); ;

            await this._zoneRepository.CustomerCreated(customer);
        }
    }
}
