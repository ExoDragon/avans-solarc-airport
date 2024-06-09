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
    public class BaggageCheckedIn : IEvent
    {
        private readonly IBaggageRepository _baggageRepository;

        public BaggageCheckedIn(IBaggageRepository baggageRepository)
        {
            _baggageRepository = baggageRepository;
        }

        public async Task Handle(string messageData)
        {
            IEnumerable<Baggage> baggages = JsonConvert.DeserializeObject<IEnumerable<Baggage>>(messageData);
            foreach(Baggage baggage in baggages)
            {
                baggage.Id = Guid.NewGuid().ToString();
                baggage.Status = "CheckedIn";

                await this._baggageRepository.BaggageCheckedIn(baggage);
            }
        }
    }
}
