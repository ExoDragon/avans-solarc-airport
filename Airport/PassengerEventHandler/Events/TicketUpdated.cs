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
    public class TicketUpdated : IEvent
    {
        private readonly IPassengerRepository _passengerRepository;

        public TicketUpdated(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task Handle(string messageData)
        {
            Ticket IncommingTicket = JsonConvert.DeserializeObject<Ticket>(messageData);
            Ticket ticket = this._passengerRepository.GetTicketById(IncommingTicket.Id);

            ticket.Code = IncommingTicket.Code;
            ticket.FlightId = IncommingTicket.FlightId;
            ticket.Airline = IncommingTicket.Airline;
            ticket.DepartureDatetime = IncommingTicket.DepartureDatetime;
            ticket.ArrivalDatetime = IncommingTicket.ArrivalDatetime;
            ticket.FlightFrom = IncommingTicket.FlightFrom;
            ticket.FlightTo = IncommingTicket.FlightTo;
            ticket.GateCode = IncommingTicket.GateCode;
            ticket.SeatNr = IncommingTicket.SeatNr;
            ticket.Status = IncommingTicket.Status;
            ticket.PassengerId = IncommingTicket.PassengerId;
            ticket.PassengerName = IncommingTicket.PassengerName;
            ticket.PassengerEmail = IncommingTicket.PassengerEmail;
            ticket.PassengerPhonenumber = IncommingTicket.PassengerPhonenumber;

            await this._passengerRepository.UpdateTicket(ticket);
        }
    }
}
