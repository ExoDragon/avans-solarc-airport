using FlightEventHandler.Domain;
using FlightEventHandler.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightEventHandler.Events
{
    public class FlightDeparted : IEvent
    {
        private readonly IFlightRepository _flightRepository;
        public FlightDeparted(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);


            Flight flight = this._flightRepository.GetFlight(jObject["Id"].ToString());
            if (flight != null)
            {
                flight.Status = "Departed";
                await this._flightRepository.FlightUpdate(flight);
            }
            else
            {
                //TODO: Error Handling?
            }
        }
    }
}
