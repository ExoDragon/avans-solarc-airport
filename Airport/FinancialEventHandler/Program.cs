using FinancialEventHandler.DBContext;
using FinancialEventHandler.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinancialEventHandler
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
                    services.AddDbContext<FinancialManagementReadDbContext>((options) => options.UseSqlServer(hostContext.Configuration.GetConnectionString("FinancialManagementReadCN")));
                    services.AddTransient<IFinancialRepository, FinacialRepository>();
                    services.RabbitMQMessageHandler(hostContext.Configuration);
                    services.Migrate();
                })
                .UseConsoleLifetime();

            return hostBuilder;
        }
    }
}
