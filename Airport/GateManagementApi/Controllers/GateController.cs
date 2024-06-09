using GateManagementApi.Commands;
using GateManagementApi.Domain;
using GateManagementApi.DTO;
using GateManagementApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateManagementApi.Handlers;

namespace GateManagementApi.Controllers
{
    [Route("api/v1/gate")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GateEmployeeOnly")]
    public class GateController : Controller
    {

        private readonly IGateRepository _gateRepository;
        private readonly IGateEventRepository _gateEventRepository;
        private readonly IMessageHandler _messageHandler;

        public GateController(IGateRepository gateRepository, IGateEventRepository gateEventRepository, IMessageHandler messageHandler)
        {
            _gateRepository = gateRepository;
            _gateEventRepository = gateEventRepository;
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_gateRepository.GetGates().Map(_gateEventRepository.GetGates()));
        }

        [HttpGet("{id}")]
        public IActionResult GetGate(string id)
        {
            try
            {
                return Ok(_gateRepository.GetGateById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateGate(Gate gate)
        {
            try
            {
                gate.Id = Guid.NewGuid().ToString();
                ICommand command = new GateCommand(JsonConvert.SerializeObject(gate), "GateCreated");
                await _gateEventRepository.GateCreated(gate, command);

                _messageHandler.Publish(command);

                return Ok("Gate Created Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}/open")]
        public async Task<IActionResult> OpenGate(string id)
        {
            try
            {
                Gate gate = _gateRepository.GetGateById(id);
                ICommand command = new GateCommand(JsonConvert.SerializeObject(gate), "GateOpened");
                await _gateEventRepository.UpdateGate(gate, command);

                _messageHandler.Publish(command);

                return Ok("Gate Successfully Opened");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseGate(string id)
        {
            try
            {
                Gate gate = _gateRepository.GetGateById(id);
                ICommand command = new GateCommand(JsonConvert.SerializeObject(gate), "GateClosed");
                await _gateEventRepository.UpdateGate(gate, command);

                _messageHandler.Publish(command);

                return Ok("Gate Successfully Closed");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("board")]
        public async Task<IActionResult> PassengerBoarded(Ticket ticket)
        {
            try
            {
                ticket.Status = "Boarded";
                ICommand command = new TicketCommand(ticket.Id, "PassengerBoarded");
                await _gateEventRepository.UpdateTicket(ticket, command);

                _messageHandler.Publish(command);

                return Ok("Passenger successfully boarded.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
