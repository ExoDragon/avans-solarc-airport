using FlightManagementApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Handlers
{
    public interface IMessageHandler
    {
        void Publish(ICommand command);
    }
}
