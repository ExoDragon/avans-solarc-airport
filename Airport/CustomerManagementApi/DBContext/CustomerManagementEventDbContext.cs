using CustomerManagementApi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementApi.DBContext
{
    public class CustomerManagementEventDbContext : DbContext
    {
        public DbSet<CustomerAggregate> Customer { get; set; }

        public CustomerManagementEventDbContext(DbContextOptions<CustomerManagementEventDbContext> contextOptions): base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
