using CustomerManagementApi.Commands;
using CustomerManagementApi.Domain;
using CustomerManagementApi.Handlers;
using CustomerManagementApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CustomerEmployeeOnly")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerEventRepository _customerEventRepository;
        private readonly IMessageHandler _messageHandler;

        public CustomerController(ICustomerRepository customerRepository, ICustomerEventRepository customerEventRepository, IMessageHandler messageHandler)
        {
            _customerRepository = customerRepository;
            _customerEventRepository = customerEventRepository;
            _messageHandler = messageHandler;
        }

        // TODO Add Events/Functions
        [HttpGet("{id}")]
        public IActionResult GetCustomer(string id)
        {
            try
            {
                return Ok(_customerRepository.GetById(id));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                return Ok(_customerRepository.GetCustomers());
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                customer.Id = Guid.NewGuid().ToString();
                ICommand command = new CustomerCommand(JsonConvert.SerializeObject(customer), "CustomerCreated");
                await _customerEventRepository.CreateCustomer(customer, command);

                _messageHandler.Publish(command);

                return Ok("Successfully created a customer");
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            try
            {
                ICommand command = new CustomerCommand(JsonConvert.SerializeObject(customer), "CustomerUpdated");
                await _customerEventRepository.UpdateCustomer(customer, command);

                _messageHandler.Publish(command);
                return Ok("Successfully updated customer");
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
