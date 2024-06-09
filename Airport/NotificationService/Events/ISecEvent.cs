using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Events
{
    public interface ISecEvent
    {
        Task Handle(string routingKey, string message);
    }
}
