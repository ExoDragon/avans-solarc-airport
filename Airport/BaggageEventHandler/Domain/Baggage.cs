using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageEventHandler.Domain
{
    public class Baggage
    {
        public string Id { get; set; }
        public int Weight { get; set; }
        public string Status { get; set; }
        public string TicketId { get; set; }
        public string FlightId { get; set; }

        public Baggage(){}

        public Baggage(string id, int weight, string status, string ticketId, string flightId)
        {
            Id = id;
            Weight = weight;
            Status = status;
            TicketId = ticketId;
            FlightId = flightId;
        }
    }
}
