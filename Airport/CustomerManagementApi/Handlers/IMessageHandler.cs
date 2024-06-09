using CustomerManagementApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Handlers
{
    public interface IMessageHandler
    {
        void Publish(ICommand command);
    }
}
