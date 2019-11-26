using System.Diagnostics.CodeAnalysis;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Extensions.ApplicationBuilder;
using SC.DevChallenge.Api.Extensions.ServiceCollection;
using Serilog;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace SC.DevChallenge.Api
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddOptions()
                    .AddDevChallengeHostedServices()
                    .AddAutoMapper(typeof(Startup).Assembly)
                    .AddDevChallengeCompression()
                    .AddDevChallengeMvc()
                    .AddDevChallengeDb(this.config)
                    .AddMemoryCache()
                    .AddDevChallengeCors()
                    .AddRouteOptions()
                    .AddDevChallengeSwagger();

        public static void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterAssemblyModules(typeof(Startup).Assembly);


        [SuppressMessage("Performance", "CA1822:Mark members as static")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseExceptionHandler(env)
               .UseStaticFiles()
               .UseSerilogRequestLogging()
               .UseRouting()
               .UseCors()
               .UseAuthentication()
               .UseEndpoints(e => e.MapControllers());
    }
}
