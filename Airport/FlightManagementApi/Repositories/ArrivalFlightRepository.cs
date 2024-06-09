using FlightManagementApi.DBContext;
using FlightManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightManagementApi.Repositories
{
    public class ArrivalFlightRepository : IArrivalFlightRepository
    {
        private readonly WebserviceContext _context;

        public ArrivalFlightRepository(WebserviceContext context)
        {
            _context = context;
        }

        public async Task<string> GetFlights()
        {
            try
            {
                return await _context.Get($"/v1/flights", "arr_iata=AMS&limit=5");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
