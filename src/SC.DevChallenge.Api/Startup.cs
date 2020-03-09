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
                    .AddAutoMapper(typeof(Startup).Assembly)
                    .AddDevChallengeCompression()
                    .AddDevChallengeMvc()
                    .AddDevChallengeProfiler()
                    .AddDevChallengeDb(this.config)
                    .AddMemoryCache()
                    .AddDevChallengeCors()
                    .AddRouteOptions()
                    .AddDevChallengeHealthChecks()
                    .AddDevChallengeSwagger();

        public static void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterAssemblyModules(typeof(Startup).Assembly);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseExceptionHandler(env)
               .UseHttpsRedirection()
               .UseStaticFiles()
               .UseDevChallengeSwaggerUi()
               .UseSerilogRequestLogging()
               .UseCors()
               .UseAuthentication()
               .UseRouting()
               .UseEndpoints(e =>
               {
                   e.MapControllers();
                   e.MapHealthChecks("/_health");
               });
    }
}
