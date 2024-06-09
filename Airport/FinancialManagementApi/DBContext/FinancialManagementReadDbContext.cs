using FinancialManagementApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi.DBContext
{
    public class FinancialManagementReadDbContext : DbContext
    {
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Lease> Lease { get; set; }

        public FinancialManagementReadDbContext(DbContextOptions<FinancialManagementReadDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
