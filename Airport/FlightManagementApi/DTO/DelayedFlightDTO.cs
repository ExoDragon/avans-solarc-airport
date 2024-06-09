using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO
{
    public class DelayedFlightDTO
    {
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }

        public DelayedFlightDTO(DateTime departureDatetime, DateTime arrivalDatetime)
        {
            DepartureDatetime = departureDatetime;
            ArrivalDatetime = arrivalDatetime;
        }
    }
}
