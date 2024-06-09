using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Commands
{
    public class TicketCommand : ICommand
    {
        private string message;
        private string routingKey;

        public TicketCommand(string message, string routingKey)
        {
            this.message = message;
            this.routingKey = routingKey;
        }

        public string GetMessage()
        {
            return message;
        }

        public string GetRoutingKey()
        {
            return routingKey;
        }
    }
}
