using FinancialManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.Repositories
{
    public interface IFinancialRepository
    {
        public IEnumerable<Invoice> GetInvoices();
        public IEnumerable<Lease> GetLeases();

        public Invoice GetById(string id);
    }
}
