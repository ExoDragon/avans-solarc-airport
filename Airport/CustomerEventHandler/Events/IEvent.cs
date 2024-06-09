using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEventHandler.Events
{
    public interface IEvent
    {
        Task Handle(string messageData);
    }
}
