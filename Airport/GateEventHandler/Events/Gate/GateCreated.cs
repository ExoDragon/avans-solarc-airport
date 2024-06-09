using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using GateEventHandler.Domain;
using GateEventHandler.Repositories;
using Newtonsoft.Json;

namespace GateEventHandler.Events
{
    public class GateCreated : IEvent
    {

        private readonly IGateRepository _gateRepository;
        public GateCreated(IGateRepository gateRepository)
        {
            _gateRepository = gateRepository;
        }

        public async Task Handle(string messageData)
        {
            Gate gate = JsonConvert.DeserializeObject<Gate>(messageData);
            await this._gateRepository.GateCreated(gate);
        }
    }
}
