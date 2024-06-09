using FinancialManagementApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagementApi.Handlers
{
    public interface IMessageHandler
    {
        void Publish(ICommand command);
    }
}
