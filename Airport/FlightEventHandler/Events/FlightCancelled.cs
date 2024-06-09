using FlightEventHandler.Domain;
using FlightEventHandler.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightEventHandler.Events
{
    public class FlightCancelled : IEvent
    {
        private readonly IFlightRepository _flightRepository;
        public FlightCancelled(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);

            Flight flight = this._flightRepository.GetFlight(jObject["Id"].ToString());
            if (flight != null)
            {
                flight.Status = "Cancelled";
                await this._flightRepository.FlightUpdate(flight);
            }
            else
            {
                //TODO: Error Handling?
            }
        }
    }
}
