using FlightManagementApi.Commands;
using FlightManagementApi.Domain;
using FlightManagementApi.DTO;
using FlightManagementApi.Handlers;
using FlightManagementApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Controllers
{
    [Route("api/v1/flight")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "FlightEmployeeOnly")]
    public class FlightController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IFlightEventRepository _flightEventRepository;
        private readonly IMessageHandler _messageHandler;

        public FlightController(IFlightRepository flightRepository, IFlightEventRepository flightEventRepository, IMessageHandler messageHandler)
        {
            _flightRepository = flightRepository;
            _flightEventRepository = flightEventRepository;
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_flightRepository.GetFlights().Map(_flightEventRepository.GetFlights()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            try
            {
                flight.Id = Guid.NewGuid().ToString();

                ICommand command = new FlightCommand(JsonConvert.SerializeObject(flight), "FlightPlanned");

                await _flightEventRepository.PlanFlight(flight, command);

                _messageHandler.Publish(command);

                return Ok("Successfully planned flight");
            } 
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}/delayed")]
        public async Task<IActionResult> Delayed(string id, DelayedFlightDTO flightDTO)
        {
            try
            {
                Flight flight = _flightRepository.GetById(id);

                flight.DepartureDatetime = flightDTO.DepartureDatetime;
                flight.ArrivalDatetime = flightDTO.ArrivalDatetime;

                flight.Status = "Delayed";

                ICommand command = new FlightCommand(JsonConvert.SerializeObject(flight), "FlightDelayed");

                await _flightEventRepository.UpdateFlight(flight, command);

                _messageHandler.Publish(command);
                return Ok("Successfully delayed flight");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}/cancelled")]
        public async Task<IActionResult> Cancelled(string id)
        {
            try
            {
                Flight flight = _flightRepository.GetById(id);

                flight.Status = "Cancelled";

                ICommand command = new FlightCommand(JsonConvert.SerializeObject(flight), "FlightCancelled");

                await _flightEventRepository.UpdateFlight(flight, command);

                _messageHandler.Publish(command);
                return Ok("Successfully cancelled flight");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}/arrived")]
        public async Task<IActionResult> Arrived(string id)
        {
            try
            {
                Flight flight = _flightRepository.GetById(id);

                flight.Status = "Arrived";

                ICommand command = new FlightCommand(JsonConvert.SerializeObject(flight), "FlightArrived");

                await _flightEventRepository.UpdateFlight(flight, command);

                _messageHandler.Publish(command);
                return Ok("Flight has been successfully arrived");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
