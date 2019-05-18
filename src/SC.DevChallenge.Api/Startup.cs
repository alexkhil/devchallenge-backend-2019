using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Extensions.AppBuilder;
using SC.DevChallenge.Api.Extensions.ServiceCollection;
using SC.DevChallenge.Configuration;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace SC.DevChallenge.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddConfiguration(configuration)
                    .AddResponceCompression()
                    .AddCustomCors()
                    .AddRouteOptions()
                    .AddCustomMvc()
                    .AddCustomSwagger();

        public static void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule(new AutofacModule());

        public void Configure(IApplicationBuilder app) =>
            app.UseResponseCompression()
               .UseExceptionHandler(environment)
               .UseCors(CorsPolicyName.AllowAny)
               .UseStaticFiles()
               .UseSwagger()
               .UseCustomSwaggerUi()
               .UseAuthentication()
               .UseMvc();
    }
}
