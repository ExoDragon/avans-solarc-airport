using FinancialEventHandler.Domain;
using FinancialEventHandler.Repositories;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinancialEventHandler.Events
{
    public class InvoiceCreated : IEvent
    {
        private readonly IFinancialRepository _financialRepository;

        public InvoiceCreated(IFinancialRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task Handle(string messageData)
        {
            Invoice invoice = JsonConvert.DeserializeObject<Invoice>(messageData);
            await this._financialRepository.SendInvoice(invoice);

        }

    }
}
