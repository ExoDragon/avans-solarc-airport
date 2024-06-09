using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain
{
    public class Flight
    {
        public string Id { get; set; }
        public string Plane { get; set; }
        public string Airline { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public string Status { get; set; }
        public string GateCode { get; set; }
        public string FlightFrom { get; set; }
        public string FlightTo { get; set; }
        public int Seats { get; set; }
    }
}
