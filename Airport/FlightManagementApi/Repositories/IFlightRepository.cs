using FlightManagementApi.Commands;
using FlightManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Repositories
{
    public interface IFlightRepository
    {
        public IEnumerable<Flight> GetFlights();
        public Flight GetById(string id);
    }
}
