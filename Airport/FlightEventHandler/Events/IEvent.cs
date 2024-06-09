using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightEventHandler.Events
{
    public interface IEvent
    {
        Task Handle(string messageData);
    }
}
