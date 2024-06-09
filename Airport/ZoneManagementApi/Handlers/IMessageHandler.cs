using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZoneManagementApi.Commands;

namespace ZoneManagementApi.Handlers
{
    public interface IMessageHandler
    {
        void Publish(ICommand command);
    }
}
