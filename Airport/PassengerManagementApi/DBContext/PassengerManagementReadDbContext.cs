using Microsoft.EntityFrameworkCore;
using PassengerManagementApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.DBContext
{
    public class PassengerManagementReadDbContext : DbContext
    {
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        public PassengerManagementReadDbContext(DbContextOptions<PassengerManagementReadDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
