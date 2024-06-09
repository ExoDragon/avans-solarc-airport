using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ZoneEventHandler.Domain;
using ZoneEventHandler.Repositories;

namespace ZoneEventHandler.Events
{
    public class CustomerLeaseStarted : IEvent
    {
        private readonly IZoneRepository _zoneRepository;

        public CustomerLeaseStarted(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task Handle(string messageData)
        {
            Lease lease = JsonConvert.DeserializeObject<Lease>(messageData);

            Zone zone = this._zoneRepository.GetZone(lease.ZoneName);
            if(zone.Status == "Inactive")
            {
                await this._zoneRepository.CustomerLeaseStarted(lease);

                zone.Status = "Active";
                await this._zoneRepository.UpdateZone(zone);
            }
            else
            {
                Console.WriteLine("Zone in Use");
                //TODO: Error Handling?
            }
        }
    }
}
