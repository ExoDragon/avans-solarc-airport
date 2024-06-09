using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using ZoneEventHandler.Repositories;
using ZoneEventHandler.Domain;


namespace ZoneEventHandler.Events
{
    public class CustomerLeaseEnded : IEvent
    {
        private readonly IZoneRepository _zoneRepository;
        public CustomerLeaseEnded(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            Lease lease = this._zoneRepository.GetLease(jObject["Id"].ToString());
            if(lease != null)
            {
                lease.Status = "Ended";
                await this._zoneRepository.CustomerLeaseEnded(lease);

                Zone zone = this._zoneRepository.GetZone(lease.ZoneName);
                zone.Status = "Inactive";
                await this._zoneRepository.UpdateZone(zone);
            }
        }
    }
}
