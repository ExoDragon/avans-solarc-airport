using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightEventHandler.DBContext;
using FlightEventHandler.Events;
using FlightEventHandler.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FlightEventHandler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile($"appsettings.json", optional: false);
                    configHost.AddEnvironmentVariables();
                    configHost.AddEnvironmentVariables("DOTNET_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<FlightManagementReadDbContext>((options) => options.UseSqlServer(hostContext.Configuration.GetConnectionString("FlightManagementReadCN")));
                    services.AddTransient<IFlightRepository, FlightRepository>();
                    services.RabbitMQMessageHandler(hostContext.Configuration);

                    services.Migrate();
                })
                .UseConsoleLifetime();

            return hostBuilder;
        }
    }
}
