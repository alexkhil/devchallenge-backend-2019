using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SC.DevChallenge.Api.Extensions.Host;
using SC.DevChallenge.DataAccess.EF;
using Serilog;

namespace SC.DevChallenge.Api
{
    public static class Program
    {
        public static Task Main(string[] args) =>
            RunAsync(CreateHostBuilder(args).Build());

        private static async Task RunAsync(IHost host)
        {
            Log.Logger = BuildLogger(host);

            try
            {
                Log.Information("Starting web host");
                await host.MigrateDatabaseAsync<AppDbContext>();
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(hostBuilder => hostBuilder.UseStartup<Startup>());

        private static ILogger BuildLogger(IHost host) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(host.Services.GetRequiredService<IConfiguration>())
                .CreateLogger();
    }
}
