using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PassengerManagementApi.Commands;
using PassengerManagementApi.Domain;
using PassengerManagementApi.Handlers;
using PassengerManagementApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Controllers
{
    [Route("api/v1/passenger")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AirportEmployeeOnly")]
    public class PassengerController : Controller
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IPassengerEventRepository _passengerEventRepository;
        private readonly IMessageHandler _messageHandler;

        public PassengerController(IPassengerRepository passengerRepository, IPassengerEventRepository passengerEventRepository, IMessageHandler messageHandler)
        {
            _passengerRepository = passengerRepository;
            _passengerEventRepository = passengerEventRepository;
            _messageHandler = messageHandler;
        }

        [HttpGet("{id}")]
        public IActionResult GetPassenger(string id)
        {
            try
            {
                return Ok(_passengerRepository.GetById(id));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult GetPassengers(string id)
        {
            try
            {
                return Ok(_passengerRepository.GetPassengers());
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(Passenger passenger)
        {
            try
            {
                passenger.Id = Guid.NewGuid().ToString();
                ICommand command = new PassengerCommand(JsonConvert.SerializeObject(passenger), "PassengerCreated");

                await _passengerEventRepository.CreatePassenger(passenger, command);

                _messageHandler.Publish(command);

                return Ok("Successfully created a passenger");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Passenger passenger)
        {
            try
            {
                ICommand command = new PassengerCommand(JsonConvert.SerializeObject(passenger), "PassengerUpdated");
                await _passengerEventRepository.UpdatePassenger(passenger, command);

                _messageHandler.Publish(command);
                return Ok("Successfully updated passenger");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}
