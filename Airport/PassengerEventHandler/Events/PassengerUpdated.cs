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
    public class PassengerUpdated : IEvent
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerUpdated(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task Handle(string messageData)
        {
            Passenger Incommingpassenger = JsonConvert.DeserializeObject<Passenger>(messageData);
            Passenger passenger = this._passengerRepository.GetById(Incommingpassenger.Id);

            passenger.Name = Incommingpassenger.Name;
            passenger.Email = Incommingpassenger.Email;
            passenger.Phonenumber = Incommingpassenger.Phonenumber;

            await this._passengerRepository.UpdatePassenger(passenger);
        }
    }
}
