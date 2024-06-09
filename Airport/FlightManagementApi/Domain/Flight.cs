using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementApi.Domain
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

        public Flight() { }

        public Flight( 
            string plane, 
            string airline, 
            DateTime departureDatetime, 
            DateTime arrivalDatetime, 
            string status, 
            string gateCode, 
            string flightFrom, 
            string flightTo,
            int seats)
        {
            Plane = plane;
            Airline = airline;
            DepartureDatetime = departureDatetime;
            ArrivalDatetime = arrivalDatetime;
            Status = status;
            GateCode = gateCode;
            FlightFrom = flightFrom;
            FlightTo = flightTo;
            Seats = seats;
        }

        public Flight(
            string id, 
            string plane,
            string airline,
            DateTime departureDatetime,
            DateTime arrivalDatetime,
            string status,
            string gateCode,
            string flightFrom,
            string flightTo,
            int seats)
        {
            Id = id;
            Plane = plane;
            Airline = airline;
            DepartureDatetime = departureDatetime;
            ArrivalDatetime = arrivalDatetime;
            Status = status;
            GateCode = gateCode;
            FlightFrom = flightFrom;
            FlightTo = flightTo;
            Seats = seats;
        }
    }
}
