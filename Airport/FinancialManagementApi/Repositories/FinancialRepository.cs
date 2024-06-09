using FinancialManagementApi.DBContext;
using FinancialManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.Repositories
{
    public class FinancialRepository : IFinancialRepository
    {
        private readonly FinancialManagementReadDbContext _context;
        public FinancialRepository(FinancialManagementReadDbContext context) 
        {
            this._context = context;
        }

        public Invoice GetById(string id)
        {
            return _context.Invoice.SingleOrDefault(f => f.Id == id);

        }

        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoice;
        }

        public IEnumerable<Lease> GetLeases()
        {
            return _context.Lease.Where(s => s.Status == "Active").ToList();
        }
    }
}
