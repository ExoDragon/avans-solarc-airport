using PassengerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public interface IPassengerRepository
    {
        public Passenger GetById(string id);
        public IEnumerable<Passenger> GetPassengers();
    }
}
