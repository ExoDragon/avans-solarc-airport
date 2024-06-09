using GateManagementApi.Domain;
using GateManagementApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.DTO
{
    public class TicketAggregateRoot : Ticket
    {
        public IList<Event> Events { get; set; }
        public int CurrentVersion { get; set; }

        public TicketAggregateRoot(
            string id,
            string code,
            string gateId,
            string GateCode,
            string status,
            string passengerId,
            string passengerName,
            string passengerEmail,
            string passengerPhonenumber,
            IList<Event> events,
            int version
        ) : base (
            id,
            code,
            gateId,
            GateCode,
            status,
            passengerId,
            passengerName,
            passengerEmail,
            passengerPhonenumber
                )
        {
            this.Events = events;
            this.CurrentVersion = version;
        }
    }
}
