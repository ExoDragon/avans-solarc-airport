using GateManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.Repositories
{
    public interface IGateRepository
    {
        public IEnumerable<Gate> GetGates();
        public IEnumerable<Ticket> GetTickets();

        public Gate GetGateById(string id);
        public Gate GetGateByCode(string code);
        public Ticket GetTicketById(string id);
    }
}
