using FlightManagementApi.Domain;
using FlightManagementApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO
{
    public class FlightAggregateRoot : Flight
    {
        public IList<Event> Events { get; set; }
        public int CurrentVersion { get; set; }

        public FlightAggregateRoot(
            string id, 
            string planeName, 
            string airline, 
            DateTime departureDatetime, 
            DateTime arrivalDatetime, 
            string status, 
            string gateCode,
            string flightFrom,
            string flightTo,
            int seats,
            IList<Event> events, 
            int version
        )
            :base(
                 id, 
                 planeName, 
                 airline, 
                 departureDatetime, 
                 arrivalDatetime, 
                 status, 
                 gateCode,
                 flightFrom,
                 flightTo,
                 seats
            )
        {
            this.Events = events;
            this.CurrentVersion = version;
        }
    }
}
