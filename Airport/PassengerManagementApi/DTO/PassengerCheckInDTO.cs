using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.DTO
{
    public class PassengerCheckInDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string TicketCode { get; set; }
        public IEnumerable<BaggageDTO> Baggage { get; set; }

        public class BaggageDTO
        {
            public string Id { get; set; }
            public int Weight { get; set; }
            public string Status { get; set; }
            public string TicketId { get; set; }
            public string FlightId { get; set; }
        }
    }
}
