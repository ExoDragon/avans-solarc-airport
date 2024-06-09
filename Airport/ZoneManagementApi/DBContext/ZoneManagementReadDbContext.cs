using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoneManagementApi.Domain;

namespace ZoneManagementApi.DBContext
{
    public class ZoneManagementReadDbContext : DbContext
    {

        public DbSet<Zone> Zone { get; set; }
        public DbSet<Lease> Lease { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public ZoneManagementReadDbContext(DbContextOptions<ZoneManagementReadDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
