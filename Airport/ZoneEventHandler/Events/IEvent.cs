using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneEventHandler.Events
{
    interface IEvent
    {
        Task Handle(string messageData); 
    }
}
