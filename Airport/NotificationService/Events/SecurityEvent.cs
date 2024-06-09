using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public class SecurityEvent : ISecEvent
    {
        public async Task Handle(string routingKey, string message)
        {
            Console.WriteLine($"SECURITY: Event {routingKey} was executed with message: {message}");
        }
    }
}
