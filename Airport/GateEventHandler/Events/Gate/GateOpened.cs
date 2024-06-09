using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using GateEventHandler.Domain;
using GateEventHandler.Repositories;


namespace GateEventHandler.Events
{
    public class GateOpened : IEvent
    {
        private readonly IGateRepository _gateRepository;
        public GateOpened(IGateRepository gateRepository)
        {
            _gateRepository = gateRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);

            Gate gate = this._gateRepository.GetGate(jObject["Id"].ToString());
            if (gate.Status == "Closed")
            {
                gate.Status = "Opened";
                await this._gateRepository.UpdateGate(gate);
            }
            else
            {
                //TODO: Error Handling?
            }
        }
    }
}
