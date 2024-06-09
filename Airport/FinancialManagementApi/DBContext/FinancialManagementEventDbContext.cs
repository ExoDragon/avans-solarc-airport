using FinancialManagementApi.Domain;
using FinancialManagementApi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.DBContext
{
    public class FinancialManagementEventDbContext : DbContext
    {
        public DbSet<InvoiceAggregate> Invoice { get; set; }

        public FinancialManagementEventDbContext(DbContextOptions<FinancialManagementEventDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
