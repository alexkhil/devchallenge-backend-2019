using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.DataAccess.EF;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;
using Serilog;
using Z.EntityFramework.Extensions;

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
                Log.Information("Starting web host");

                var timer = new Stopwatch();
                timer.Start();
                using (var scope = webHost.Services.CreateScope())
                {
                    EntityFrameworkManager.ContextFactory = context => scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    var inputDataPath = Path.Combine(AppContext.BaseDirectory, $"Input{Path.DirectorySeparatorChar}data.csv");
                    await dbInitializer.InitializeAsync(inputDataPath);
                }
                timer.Stop();

                Log.Information("Time spent: {time} ms", timer.ElapsedMilliseconds);

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
