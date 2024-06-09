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
    public class GateClosed : IEvent
    {

        private readonly IGateRepository _gateRepository;
        public GateClosed(IGateRepository gateRepository)
        {
            _gateRepository = gateRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);

            Gate gate = this._gateRepository.GetGate(jObject["Id"].ToString());
            if (gate.Status == "Opened")
            {
                gate.Status = "Closed";
                await this._gateRepository.UpdateGate(gate);
            }
            else
            {
                //TODO: Error Handling?
            }
        }
    }
}
