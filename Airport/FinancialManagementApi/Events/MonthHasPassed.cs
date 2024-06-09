using FinancialManagementApi.Commands;
using FinancialManagementApi.Domain;
using FinancialManagementApi.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.Events
{
    public class MonthHasPassed : IEvent
    {
        private readonly IConfiguration _config;
        private readonly IModel _channel;
        private readonly IFinancialEventRepository _financialEventRepository;
        private readonly IFinancialRepository _financialRepository;

        public MonthHasPassed(IConfiguration config, IModel channel, IFinancialEventRepository financialEventRepository, IFinancialRepository financialRepository)
        {
            _config = config;
            _channel = channel;
            _financialEventRepository = financialEventRepository;
            _financialRepository = financialRepository;
        }

        public async Task Handle(string messageData)
        {
            IEnumerable<Lease> leases = this._financialRepository.GetLeases();
            List<Invoice> invoices = new List<Invoice>();
            foreach (Lease lease in leases)
            {
                Invoice invoice = new Invoice()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerName = lease.CustomerName,
                    CustomerEmail = lease.CustomerEmail,
                    ZoneName = lease.ZoneName,
                    ZoneRentalPrice = lease.Price,
                    BillingDate = DateTime.Now
                };

                invoices.Add(invoice);
            }
            string serializedInvoices = JsonConvert.SerializeObject(invoices);

            ICommand command = new InvoiceCommand(serializedInvoices, "InvoicesCreated");
            await this._financialEventRepository.SendMonthlyInvoices(invoices, command);

            _channel.ExchangeDeclare(exchange: _config["RabbitMQHandler:Exchange"], type: "topic");

            var routingKey = command.GetRoutingKey();
            var message = command.GetMessage();
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _config["RabbitMQHandler:Exchange"],
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
        }
    }
}

