using System;
using System.IO;
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
        public static int Main(string[] args) =>
            LoadAndRun(CreateWebHostBuilder(args).Build());

        public static int LoadAndRun(IWebHost webHost)
        {
            Log.Logger = BuildLogger(webHost);

            try
            {
                Log.Information("Starting web host");
                using (var scope = webHost.Services.CreateScope())
                {
                    EntityFrameworkManager.ContextFactory = context => scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    var inputDataPath = Path.Combine(AppContext.BaseDirectory, $"Input{Path.DirectorySeparatorChar}data.csv");
                    dbInitializer.InitializeAsync(inputDataPath).GetAwaiter().GetResult();
                }
                webHost.Run();
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
                   .UseStartup<Startup>();

        private static ILogger BuildLogger(IWebHost webHost) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(webHost.Services.GetRequiredService<IConfiguration>())
                .CreateLogger();
    }
}
