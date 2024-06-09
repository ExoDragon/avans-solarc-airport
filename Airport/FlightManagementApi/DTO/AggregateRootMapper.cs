using FlightManagementApi.Domain;
using FlightManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO
{
    public static class AggregateRootMapper
    {
        public static IEnumerable<FlightAggregateRoot> Map(this IEnumerable<Flight> flights, IEnumerable<FlightAggregate> flightAggregates)
        {
            List<FlightAggregateRoot> aggregateRoot = new List<FlightAggregateRoot>();

            foreach (Flight flight in flights)
            {
                FlightAggregate aggregate = flightAggregates.SingleOrDefault(f => f.Id == flight.Id);

                aggregateRoot.Add(new FlightAggregateRoot(
                        flight.Id, 
                        flight.Plane, 
                        flight.Airline, 
                        flight.DepartureDatetime, 
                        flight.ArrivalDatetime, 
                        flight.Status, 
                        flight.GateCode, 
                        flight.FlightFrom,
                        flight.FlightTo,
                        flight.Seats,
                        aggregate.Events, 
                        aggregate.CurrentVersion
                    )
                );
            }

            return aggregateRoot;
        }
    }
}
