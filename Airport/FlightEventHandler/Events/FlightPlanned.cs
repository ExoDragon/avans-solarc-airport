using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightEventHandler.Repositories;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FlightEventHandler.Domain;

namespace FlightEventHandler.Events
{
    public class FlightPlanned : IEvent
    {

        private readonly IFlightRepository _flightRepository;
        public FlightPlanned(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task Handle(string messageData)
        {
            Flight flight = JsonConvert.DeserializeObject<Flight>(messageData);

            await this._flightRepository.FlightPlanned(flight);
        }
    }
}
