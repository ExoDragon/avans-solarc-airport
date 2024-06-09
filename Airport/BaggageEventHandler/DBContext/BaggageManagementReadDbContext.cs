using BaggageEventHandler.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageEventHandler.DBContext
{
    public class BaggageManagementReadDbContext : DbContext
    {
        public DbSet<Baggage> Baggage { get; set; }

        public BaggageManagementReadDbContext(DbContextOptions<BaggageManagementReadDbContext> contextOptions): base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
