using FinancialEventHandler.Domain;
using FinancialEventHandler.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinancialEventHandler.Events
{
    public class InvoicesCreated : IEvent
    {
        private readonly IFinancialRepository _financialRepository;

        public InvoicesCreated(IFinancialRepository financialRepository)
        {
            this._financialRepository = financialRepository;
        }

        public async Task Handle(string messageData)
        {
            List<Invoice> invoices = JsonConvert.DeserializeObject<List<Invoice>>(messageData);
            await this._financialRepository.SendMonthlyInvoices(invoices);
        }
    }
}

