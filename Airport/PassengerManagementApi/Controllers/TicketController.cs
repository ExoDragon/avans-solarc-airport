using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PassengerManagementApi.Commands;
using PassengerManagementApi.Domain;
using PassengerManagementApi.DTO;
using PassengerManagementApi.Handlers;
using PassengerManagementApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Controllers
{
    [Route("api/v1/ticket")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AirportEmployeeOnly")]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEventRepository _ticketEventRepository;
        private readonly IMessageHandler _messageHandler;

        public TicketController(ITicketRepository ticketRepository, ITicketEventRepository ticketEventRepository, IMessageHandler messageHandler)
        {
            _ticketRepository = ticketRepository;
            _ticketEventRepository = ticketEventRepository;
            _messageHandler = messageHandler;
        }

        [HttpGet("{id}")]
        public IActionResult GetTicket(string id)
        {
            try
            {
                return Ok(_ticketRepository.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult GetTickets()
        {
            try
            {
                return Ok(_ticketRepository.GetTickets());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}/flight")]
        public IActionResult GetTicketsByFlight(string id)
        {
            try
            {
                return Ok(_ticketRepository.GetTicketsByFlightId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}/passenger")]
        public IActionResult GetTicketsByPassenger(string id)
        {
            try
            {
                return Ok(_ticketRepository.GetTicketsByPassengerId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                ticket.Id = Guid.NewGuid().ToString();
                ICommand command = new TicketCommand(JsonConvert.SerializeObject(ticket), "TicketCreated");
                await _ticketEventRepository.CreateTicket(ticket, command);

                _messageHandler.Publish(command);

                return Ok("Successfully created a ticket");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicket(Ticket ticket)
        {
            try
            {
                ICommand command = new TicketCommand(JsonConvert.SerializeObject(ticket), "TicketUpdated");
                await _ticketEventRepository.UpdateTicket(ticket, command);

                _messageHandler.Publish(command);
                return Ok("Successfully updated Ticket");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckInPassenger(PassengerCheckInDTO passengerCheckInDTO)
        {
            try
            {
                Ticket ticket = _ticketRepository.GetByTicketCode(passengerCheckInDTO.TicketCode);

                ticket.Status = "CheckedIn";
                ICommand command = new TicketCommand(JsonConvert.SerializeObject(ticket), "TicketUpdated");
                await _ticketEventRepository.UpdateTicket(ticket, command);

                _messageHandler.Publish(command);

                ICommand baggageCommand = new BaggageCommand(JsonConvert.SerializeObject(passengerCheckInDTO.Baggage), "BaggageCheckedIn");
                _messageHandler.Publish(baggageCommand);

                return Ok("Successfully updated Ticket");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
