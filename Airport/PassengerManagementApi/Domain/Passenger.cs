using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Domain
{
    public class Passenger
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
    }
}
