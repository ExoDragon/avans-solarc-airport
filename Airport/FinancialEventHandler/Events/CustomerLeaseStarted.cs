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
    public class CustomerLeaseStarted : IEvent
    {
        private readonly IFinancialRepository _financialRepository;

        public CustomerLeaseStarted(IFinancialRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task Handle(string messageData)
        {
            Lease lease = JsonConvert.DeserializeObject<Lease>(messageData);
            lease.Id = Guid.NewGuid().ToString();

            Zone zone = this._financialRepository.GetZone(lease.ZoneName);
            if (zone.Status.Equals("Inactive"))
            {
                await this._financialRepository.AddLease(lease);
                zone.Status = "Active";
                await this._financialRepository.UpdateZone(zone);
            }
            else 
            {
                Console.WriteLine("Zone in Use");
            }
        }
    }
}
