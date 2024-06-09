using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightEventHandler.DBContext;
using FlightEventHandler.Domain;

namespace FlightEventHandler.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightManagementReadDbContext _context;
        public FlightRepository(FlightManagementReadDbContext context)
        {
            _context = context;
        }

        public Flight GetFlight(string id)
        {
            Flight flight = this._context.Flight.SingleOrDefault(f => f.Id == id);
            return flight;
        }

        public async Task FlightPlanned(Flight flight)
        {
            this._context.Flight.Add(flight);
            await this._context.SaveChangesAsync();
        }

        public async Task FlightUpdate(Flight flight)
        {
            this._context.Flight.Update(flight);
            await this._context.SaveChangesAsync();
        }
    }
}
