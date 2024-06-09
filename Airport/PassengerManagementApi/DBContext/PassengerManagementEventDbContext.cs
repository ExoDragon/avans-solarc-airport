using Microsoft.EntityFrameworkCore;
using PassengerManagementApi.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerManagementApi.DBContext
{
    public class PassengerManagementEventDbContext : DbContext
    {
        public DbSet<PassengerAggregate> Passenger { get; set; }
        public DbSet<TicketAggregate> Ticket { get; set; }

        public PassengerManagementEventDbContext(DbContextOptions<PassengerManagementEventDbContext> contextOptions): base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
