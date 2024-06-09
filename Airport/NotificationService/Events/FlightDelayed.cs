using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public class FlightDelayed : IEvent
    {
        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            var from = jObject["FlightFrom"].ToString();
            var to = jObject["FlightTo"].ToString();
            var departTime = jObject["DepartureDatetime"];

            Console.WriteLine($"Dear passenger, the flight from {from} to {to} has been delayed. The new departure time will be {departTime}.");
        }
    }
}
