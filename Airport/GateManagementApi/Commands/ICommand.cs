using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Commands
{
    public interface ICommand
    {
        string GetRoutingKey();
        string GetMessage();
    }
}
