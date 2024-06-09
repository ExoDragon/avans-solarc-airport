using CustomerEventHandler.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEventHandler.DBContext
{
    public class CustomerManagementReadDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        public CustomerManagementReadDbContext(DbContextOptions<CustomerManagementReadDbContext> contextOptions): base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
