using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialEventHandler.Domain;
using FinancialEventHandler.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinancialEventHandler.Events
{
    public class ZoneCreated : IEvent
    {
        private readonly IFinancialRepository _financialRepository;

        public ZoneCreated(IFinancialRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task Handle(string messageData)
        {
            {
                Zone zone = JsonConvert.DeserializeObject<Zone>(messageData);
                zone.Id = Guid.NewGuid().ToString();

                await this._financialRepository.CreateZone(zone);
            }
        }
    }
}
