using FinancialEventHandler.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialEventHandler.DBContext
{
    public class FinancialManagementReadDbContext : DbContext
    {
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<Lease> Lease { get; set; }

        public FinancialManagementReadDbContext(DbContextOptions<FinancialManagementReadDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
