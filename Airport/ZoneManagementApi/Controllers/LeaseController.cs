using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoneManagementApi.Commands;
using ZoneManagementApi.Domain;
using ZoneManagementApi.DTO;
using ZoneManagementApi.Handlers;
using ZoneManagementApi.Repositories;

namespace ZoneManagementApi.Controllers
{
    [Route("api/v1/lease")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ZoneEmployeeOnly")]
    public class LeaseController : Controller
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneEventRepository _zoneEventRepository;
        private readonly IMessageHandler _messageHandler;

        public LeaseController(IZoneRepository zoneRepository, IZoneEventRepository zoneEventRepository, IMessageHandler messageHandler)
        {
            _zoneRepository = zoneRepository;
            _zoneEventRepository = zoneEventRepository;
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_zoneRepository.GetLeases().Map(_zoneEventRepository.GetLeases()));
        }

        [HttpGet("{id}")]
        public IActionResult GetLease(string id)
        {
            try
            {
                return Ok(_zoneRepository.GetLeaseById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LeaseZone(Lease lease)
        {
            try
            {
                lease.Id = Guid.NewGuid().ToString();
                ICommand command = new LeaseCommand(JsonConvert.SerializeObject(lease), "CustomerLeaseStarted");
                await _zoneEventRepository.LeaseZone(lease, command);

                _messageHandler.Publish(command);

                return Ok("Successfully Created Lease");                    
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("{id}/cancel")]
        [HttpPost]
        public async Task<IActionResult> CancelLease(string id)
        {
            try
            {
                Lease lease = _zoneRepository.GetLeaseById(id);
                ICommand command = new LeaseCommand(JsonConvert.SerializeObject(lease), "CustomerLeaseEnded");
                await _zoneEventRepository.UpdateLease(lease, command);

                _messageHandler.Publish(command);

                return Ok("Lease Successfully Ended");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



    }
}
