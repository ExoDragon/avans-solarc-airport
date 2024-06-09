using BaggageEventHandler.DBContext;
using BaggageEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageEventHandler.Repositories
{
    public class BaggageRepository : IBaggageRepository
    {
        private readonly BaggageManagementReadDbContext _context;

        public BaggageRepository(BaggageManagementReadDbContext context)
        {
            _context = context;
        }

        public async Task BaggageCheckedIn(Baggage baggage)
        {
            this._context.Baggage.Add(baggage);
            await this._context.SaveChangesAsync();
        }

        public async Task BaggageUpdate(Baggage baggage)
        {
            this._context.Baggage.Update(baggage);
            await this._context.SaveChangesAsync();
        }

        public Baggage GetById(string id)
        {
            return this._context.Baggage.SingleOrDefault(b => b.Id == id);
        }
    }
}
