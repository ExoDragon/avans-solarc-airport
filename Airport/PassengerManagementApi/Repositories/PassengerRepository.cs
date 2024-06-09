using PassengerManagementApi.DBContext;
using PassengerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly PassengerManagementReadDbContext _context;

        public PassengerRepository(PassengerManagementReadDbContext context)
        {
            _context = context;
        }

        public Passenger GetById(string id)
        {
            return _context.Passenger.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Passenger> GetPassengers()
        {
            return _context.Passenger;
        }
    }
}
