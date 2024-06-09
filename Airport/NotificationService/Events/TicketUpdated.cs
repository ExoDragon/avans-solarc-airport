using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public class TicketUpdated : IEvent
    {
        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            var status = jObject["Status"].ToString();

            if(status == "Validated")
            {
                var flight = jObject["FlightId"].ToString();
                var passenger = jObject["PassengerName"].ToString();

                Console.WriteLine($"BORDER SECURITY: Passenger {passenger} has boarded flight {flight}.");
            }            
        }
    }
}
