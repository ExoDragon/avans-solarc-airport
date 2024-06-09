using FlightManagementApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementApi.DBContext
{
    public class FlightManagementReadDbContext : DbContext
    {

        public DbSet<Flight> Flight { get; set; }

        public FlightManagementReadDbContext(DbContextOptions<FlightManagementReadDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
