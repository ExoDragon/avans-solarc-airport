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
    public class ZoneCreated : IEvent
    {
        private readonly IZoneRepository _zoneRepository;
        public ZoneCreated(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }
        public async Task Handle(string messageData)
        {
            Zone zone = JsonConvert.DeserializeObject<Zone>(messageData);
            await this._zoneRepository.ZoneCreated(zone);
        }
    }
}
