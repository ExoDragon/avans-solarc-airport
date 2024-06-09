using PassengerManagementApi.Commands;
using PassengerManagementApi.Domain;
using PassengerManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public interface IPassengerEventRepository
    {
        public PassengerAggregate GetById(string id);
        public IEnumerable<PassengerAggregate> GetPassengers();
        public Task CreatePassenger(Passenger passenger, ICommand command);
        public Task UpdatePassenger(Passenger passenger, ICommand command);
    }
}
