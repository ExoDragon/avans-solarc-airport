using BaggageEventHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageEventHandler.Repositories
{
    public interface IBaggageRepository
    {
        public Baggage GetById(string id);
        public Task BaggageCheckedIn(Baggage baggage);
        public Task BaggageUpdate(Baggage baggage);
    }
}
