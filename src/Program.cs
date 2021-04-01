using System;
using System.Threading.Tasks;
using Atriarch.ScheduledTasks.Worker.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Atriarch.ScheduledTasks.Worker
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                .UseConsoleLifetime()
                .Build()
                .RunAsync();
        }
        private static IConfiguration Configuration;

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables(prefix: "Atriarch_");
                    Configuration = config.Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddHostedService<BasicSimcWorker>();
                });
    }
}
