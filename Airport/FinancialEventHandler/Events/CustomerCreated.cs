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
    public class CustomerCreated : IEvent
    {
        private readonly IFinancialRepository _financialRepository;

        public CustomerCreated(IFinancialRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }


        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            Customer customer = new Customer(
                Guid.NewGuid().ToString(),
                jObject["Name"].ToString(),
                jObject["Email"].ToString()
            );

            await this._financialRepository.AddCustomer(customer);
        }
    }
}
