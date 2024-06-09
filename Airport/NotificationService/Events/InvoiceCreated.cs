using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NotificationService.Domain;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public class InvoiceCreated : IEvent
    {
        private readonly ServiceProvider _serviceProvider;
        public InvoiceCreated(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(string messageData)
        {
            IConfiguration config = this._serviceProvider.GetService<IConfiguration>();
            Invoice invoice = JsonConvert.DeserializeObject<Invoice>(messageData);
            MailHandler mailHandler = new MailHandler(config);
            mailHandler.SendEmail(invoice);
        }

    }
}
