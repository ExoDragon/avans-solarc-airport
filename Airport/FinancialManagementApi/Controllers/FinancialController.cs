using FinancialManagementApi.Commands;
using FinancialManagementApi.Domain;
using FinancialManagementApi.Handlers;
using FinancialManagementApi.Repositories;
using FlightManagementApi.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagementApi.Controllers
{
    [Route("api/v1/invoices")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "FinancialEmployeeOnly")]

    public class FinancialController : Controller
    {
        private readonly IFinancialRepository _financialRepository;
        private readonly IFinancialEventRepository _financialEventRepository;
        private readonly IMessageHandler _messageHandler;

        public FinancialController(IFinancialRepository financialRepository, IFinancialEventRepository financialEventRepository, IMessageHandler messageHandler)
        {
            this._financialRepository = financialRepository;
            this._financialEventRepository = financialEventRepository;
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_financialRepository.GetInvoices().Map(_financialEventRepository.GetInvoices()));
        }

        [Route("sendInvoice")]
        [HttpPost]
        public async Task<IActionResult> SendInvoice(Invoice invoice)
        {
            try
            {
                invoice.Id = Guid.NewGuid().ToString();

                ICommand command = new InvoiceCommand(JsonConvert.SerializeObject(invoice), "InvoiceCreated");

                await _financialEventRepository.SendInvoice(invoice, command);

                _messageHandler.Publish(command);

                return Ok("Successfully send invoice");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("sendMonthlyInvoice")]
        [HttpPost]
        public async Task<IActionResult> SendMonthlyInvoice(List<Invoice> invoices)
        {
            try
            {
                foreach (Invoice invoice in invoices)
                {
                    invoice.Id = Guid.NewGuid().ToString();
                }

                ICommand command = new InvoiceCommand(JsonConvert.SerializeObject(invoices), "InvoicesCreated");

                await _financialEventRepository.SendMonthlyInvoices(invoices, command);

                _messageHandler.Publish(command);

                return Ok("Successfully send invoices");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

