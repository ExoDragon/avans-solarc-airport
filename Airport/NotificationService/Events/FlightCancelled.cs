using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public class FlightCancelled : IEvent
    {
        public async Task Handle(string messageData)
        {
            JObject jObject = JObject.Parse(messageData);
            var from = jObject["FlightFrom"].ToString();
            var to = jObject["FlightTo"].ToString();

            Console.WriteLine($"Dear passenger, the flight from {from} to {to} has been cancelled.");
        }
    }
}
