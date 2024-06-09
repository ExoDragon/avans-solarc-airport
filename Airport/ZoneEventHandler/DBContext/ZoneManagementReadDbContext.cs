using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ZoneEventHandler.Domain;

namespace ZoneEventHandler.DBContext
{
    public class ZoneManagementReadDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Lease> Lease { get; set; }
        public DbSet<Zone> Zone { get; set; }

        public ZoneManagementReadDbContext(DbContextOptions<ZoneManagementReadDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        } 
    }
}
