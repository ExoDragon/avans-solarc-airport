using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.Commands
{
    public class CustomerCommand : ICommand
    {
        private string message;
        private string routingKey;

        public CustomerCommand(string message, string routingKey)
        {
            this.message = message;
            this.routingKey = routingKey;
        }

        public string GetMessage()
        {
            return this.message;
        }

        public string GetRoutingKey()
        {
            return this.routingKey;
        }
    }
}
