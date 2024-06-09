using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateEventHandler.Domain
{
    public class Ticket
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string GateId { get; set; }
        public string GateCode { get; set; }
        public string Status { get; set; }
        public string PassengerId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerEmail { get; set; }
        public string PassengerPhonenumber { get; set; }

        public Ticket(string id, string code, string gateId, string gateCode, string status, string passengerId, string passengerName, string passengerEmail, string passengerPhonenumber)
        {
            Id = id;
            Code = code;
            GateId = gateId;
            GateCode = gateCode;
            Status = status;
            PassengerId = passengerId;
            PassengerName = passengerName;
            PassengerEmail = passengerEmail;
            PassengerPhonenumber = passengerPhonenumber;
        }
        public Ticket() { }

    }
}
