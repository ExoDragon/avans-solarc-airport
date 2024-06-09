using FlightManagementApi.Commands;
using FlightManagementApi.Domain;
using FlightManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Repositories
{
    public interface IFlightEventRepository
    {
        public Task PlanFlight(Flight flight, ICommand command);
        public Task UpdateFlight(Flight flight, ICommand command);
        public IEnumerable<FlightAggregate> GetFlights();
    }
}
