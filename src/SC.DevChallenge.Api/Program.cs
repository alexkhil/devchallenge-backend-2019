using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace SC.DevChallenge.Api
{
    public static class Program
    {
        public static Task<int> Main(string[] args) =>
            LoadAndRun(CreateWebHostBuilder(args).Build());

        public static async Task<int> LoadAndRun(IWebHost webHost)
        {
            Log.Logger = BuildLogger(webHost);

            try
            {
                //Log.Information("Initing db");
                //await webHost.InitDbAsync();

                Log.Information("Starting web host");
                await webHost.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseSerilog()
                   .ConfigureServices(services => services.AddAutofac())
                   .UseStartup<Startup>();

        private static ILogger BuildLogger(IWebHost webHost) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(webHost.Services.GetRequiredService<IConfiguration>())
                .CreateLogger();
    }
}
