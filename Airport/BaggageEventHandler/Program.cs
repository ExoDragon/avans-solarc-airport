using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaggageEventHandler.DBContext;
using BaggageEventHandler.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BaggageEventHandler
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
                    services.AddDbContext<BaggageManagementReadDbContext>((options) => options.UseSqlServer(hostContext.Configuration.GetConnectionString("BaggageManagementReadCN")));
                    services.AddTransient<IBaggageRepository, BaggageRepository>();
                    services.RabbitMQMessageHandler(hostContext.Configuration);
                    services.Migrate();
                })
                .UseConsoleLifetime();

            return hostBuilder;
        }
    }
}
