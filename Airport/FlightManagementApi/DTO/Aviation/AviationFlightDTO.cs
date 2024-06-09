using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO.Aviation
{
    public class AviationFlightDTO
    {
        public string number { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
    }
}
