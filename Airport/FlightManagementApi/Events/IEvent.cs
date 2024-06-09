using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Events
{
    public interface IEvent
    {
        Task Handle(string messageData);
    }
}
