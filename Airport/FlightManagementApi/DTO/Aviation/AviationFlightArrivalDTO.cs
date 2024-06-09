using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO.Aviation
{
    public class AviationFlightArrivalDTO
    {
        public string airport { get; set; }
        public string timezone { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
        public string terminal { get; set; }
        public string gate { get; set; }
        public string delay { get; set; }
        public DateTime scheduled { get; set; }
        public DateTime estimated { get; set; }
    }
}
