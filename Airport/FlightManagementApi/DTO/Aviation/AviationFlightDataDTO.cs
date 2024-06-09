using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO.Aviation
{
    public class AviationFlightDataDTO
    {
        public DateTime flight_date { get; set; }
        public string flight_status { get; set; }
        public AviationFlightDepartureDTO departure { get; set; }
        public AviationFlightArrivalDTO arrival { get; set; }
        public AviationFlightAirlineDTO airline { get; set; }
        public AviationFlightDTO flight { get; set; }
    }
}
