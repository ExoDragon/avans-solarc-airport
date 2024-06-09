using GateManagementApi.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateManagementApi.DBContext
{
    public class GateManagementEventDbContext : DbContext
    {
        public DbSet<GateAggregate> Gate { get; set; }
        public DbSet<TicketAggregate> Ticket { get; set; }

        public GateManagementEventDbContext(DbContextOptions<GateManagementEventDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
