using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GateEventHandler.Domain;

namespace GateEventHandler.DBContext
{
    public class GateManagementReadDbContext : DbContext
    {

        public DbSet<Gate> Gate { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        public GateManagementReadDbContext(DbContextOptions<GateManagementReadDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
