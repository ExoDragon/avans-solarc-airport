using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoneManagementApi.Domain.Aggregate;

namespace ZoneManagementApi.DBContext
{
    public class ZoneManagementEventDbContext: DbContext
    {

        public DbSet<ZoneAggregate> Zone { get; set; }
        public DbSet<LeaseAggregate> Lease { get; set; }

        public ZoneManagementEventDbContext(DbContextOptions<ZoneManagementEventDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
