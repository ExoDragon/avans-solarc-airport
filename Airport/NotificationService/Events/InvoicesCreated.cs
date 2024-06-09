using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public class InvoicesCreated : IEvent
    {
        private readonly ServiceProvider _serviceProvider;
        public InvoicesCreated(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(string messageData)
        {
            IConfiguration config = this._serviceProvider.GetService<IConfiguration>();
            List<Invoice> invoices = JsonConvert.DeserializeObject<List<Invoice>>(messageData);
            foreach (Invoice inv in invoices)
            {
                MailHandler mailHandler = new MailHandler(config);
                mailHandler.SendEmail(inv);
            }
        }
    }
}

