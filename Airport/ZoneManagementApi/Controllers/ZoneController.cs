using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    [Route("api/v1/zone")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ZoneEmployeeOnly")]
    public class ZoneController : Controller
    {

        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneEventRepository _zoneEventRepository;
        private readonly IMessageHandler _messageHandler;

        public ZoneController(IZoneRepository zoneRepository, IZoneEventRepository zoneEventRepository, IMessageHandler messageHandler)
        {
            _zoneRepository = zoneRepository;
            _zoneEventRepository = zoneEventRepository;
            _messageHandler = messageHandler;
        }


        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return Ok(_zoneRepository.GetZones());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetZone(string id)
        {
            try
            {
                return Ok(_zoneRepository.GetZoneById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Zone zone)
        {
            try
            {
                zone.Id = Guid.NewGuid().ToString();
                ICommand command = new ZoneCommand(JsonConvert.SerializeObject(zone), "ZoneCreated");
                await _zoneEventRepository.ZoneCreated(zone, command);

                _messageHandler.Publish(command);

                return Ok("Successfully Created Zone");

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
