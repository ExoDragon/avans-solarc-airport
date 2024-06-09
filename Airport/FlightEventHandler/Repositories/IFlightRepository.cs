using FlightEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightEventHandler.Repositories
{
    public interface IFlightRepository
    {
        public Flight GetFlight(string id);
        public Task FlightPlanned(Flight flight);
        public Task FlightUpdate(Flight flight);
    }
}
