using Newtonsoft.Json;
using PassengerEventHandler.Domain;
using PassengerEventHandler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerEventHandler.Events
{
    public class PassengerCreated : IEvent
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerCreated(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task Handle(string messageData)
        {
            Passenger passenger = JsonConvert.DeserializeObject<Passenger>(messageData);
            await this._passengerRepository.CreatePassenger(passenger);
        }
    }
}
