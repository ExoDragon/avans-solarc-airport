using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain
{
    public class Ticket
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string FlightId { get; set; }
        public string Airline { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public string FlightFrom { get; set; }
        public string FlightTo { get; set; }
        public string GateCode { get; set; }
        public int SeatNr { get; set; }
        public string Status { get; set; }
        public string PassengerId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerEmail { get; set; }
        public string PassengerPhonenumber { get; set; }
    }
}
