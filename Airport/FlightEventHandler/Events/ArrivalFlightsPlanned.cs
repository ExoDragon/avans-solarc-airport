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
    class ArrivalFlightsPlanned : IEvent
    {
        private readonly IFlightRepository _flightRepository;
        public ArrivalFlightsPlanned(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task Handle(string messageData)
        {
            List<Flight> flights = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Flight>>(messageData);

            foreach (Flight flight in flights)
            {
                await _flightRepository.FlightPlanned(flight);
            }
        }
    }
}
