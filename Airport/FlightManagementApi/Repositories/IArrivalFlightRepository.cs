using FlightManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Repositories
{
    public interface IArrivalFlightRepository
    {
        public Task<string> GetFlights();
    }
}
