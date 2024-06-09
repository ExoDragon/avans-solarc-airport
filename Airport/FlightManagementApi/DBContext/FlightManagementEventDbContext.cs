using FlightManagementApi.Domain;
using FlightManagementApi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementApi.DBContext
{
    public class FlightManagementEventDbContext : DbContext
    {

        public DbSet<FlightAggregate> Flight { get; set; }

        public FlightManagementEventDbContext(DbContextOptions<FlightManagementEventDbContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
