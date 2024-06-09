using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GateManagementApi.Commands;

namespace GateManagementApi.Handlers
{
    public interface IMessageHandler
    {
        void Publish(ICommand command);
    }
}
