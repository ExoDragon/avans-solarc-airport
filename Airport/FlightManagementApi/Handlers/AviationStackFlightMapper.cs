using FlightManagementApi.Domain;
using FlightManagementApi.DTO.Aviation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Handlers
{
    public class AviationStackFlightMapper : IFlightMapper
    {
        public IEnumerable<Flight> MapFlights(string result)
        {
            AviationResultDTO aviationResult = JsonConvert.DeserializeObject<AviationResultDTO>(result);

            List<Flight> flights = new List<Flight>();

            foreach (AviationFlightDataDTO flight in aviationResult.data)
            {
                flights.Add(new Flight(
                        Guid.NewGuid().ToString(),
                        flight.flight.icao,
                        flight.airline.name,
                        flight.departure.estimated,
                        flight.arrival.estimated,
                        flight.flight_status,
                        flight.arrival.gate,
                        flight.departure.airport,
                        flight.arrival.airport,
                        0
                    )
                );
            }

            return flights;
        }
    }
}
