using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerEventHandler.DBContext
{
    public static class CreateMigrations 
    {
        public static void Migrate(this IServiceCollection services)
        {
            try
            {
                var serviceProvider = services.BuildServiceProvider();
                PassengerManagementReadDbContext readDbContext = serviceProvider.GetService<PassengerManagementReadDbContext>();
                readDbContext.Database.Migrate();
            }
            catch { }
        }
    }
}
