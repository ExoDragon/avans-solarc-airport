using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementApi.Commands;
using FlightManagementApi.DBContext;
using FlightManagementApi.Domain;
using FlightManagementApi.Domain.Aggregates;
using FlightManagementApi.Domain.Core;
using Newtonsoft.Json;

namespace FlightManagementApi.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightManagementReadDbContext _context;
        public FlightRepository(FlightManagementReadDbContext context)
        {
            _context = context;
        }

        public Flight GetById(string id)
        {
            return _context.Flight.SingleOrDefault(f => f.Id == id);
        }

        public IEnumerable<Flight> GetFlights()
        {
            return _context.Flight;
        }
    }
}
