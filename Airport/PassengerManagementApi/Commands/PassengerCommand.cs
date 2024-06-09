using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Commands
{
    public class PassengerCommand : ICommand
    {
        private string message;
        private string routingKey;

        public PassengerCommand(string message, string routingKey)
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
