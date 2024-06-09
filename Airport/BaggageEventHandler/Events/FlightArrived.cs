using BaggageEventHandler.Domain;
using BaggageEventHandler.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageEventHandler.Events
{
    public class FlightArrived : IEvent
    {
        private readonly IBaggageRepository _baggageRepository;

        public FlightArrived(IBaggageRepository baggageRepository)
        {
            _baggageRepository = baggageRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            Baggage baggage = this._baggageRepository.GetById(jObject["Id"].ToString());

            if (baggage != null && baggage.Status.Equals("OnPlane"))
            {
                baggage.Status = "TransferredToClaimFacility";
                await this._baggageRepository.BaggageUpdate(baggage);
            }
            else
            {
                // Error handling?
            }
        }
    }
}
