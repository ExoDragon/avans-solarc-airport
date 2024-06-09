using FlightManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Handlers
{
    public interface IFlightMapper
    {
        public IEnumerable<Flight> MapFlights(string result);
    }
}
