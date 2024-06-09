using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialEventHandler.Domain;
using FinancialEventHandler.Repositories;
using Newtonsoft.Json.Linq;


namespace FinancialEventHandler.Events
{
    public class CustomerLeaseEnded : IEvent
    {
        private readonly IFinancialRepository _financialRepository;

        public CustomerLeaseEnded(IFinancialRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);

            Lease lease = this._financialRepository.GetLeaseByZoneCode(jObject["ZoneName"].ToString());

            if (lease != null)
            {
                lease.Status = "Ended";
                await this._financialRepository.EndLease(lease);

                Zone zone = this._financialRepository.GetZone(lease.ZoneName);
                zone.Status = "Inactive";
                await this._financialRepository.UpdateZone(zone);
            }
        }
    }
}
