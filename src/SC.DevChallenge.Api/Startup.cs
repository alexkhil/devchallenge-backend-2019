using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Extensions.AppBuilder;
using SC.DevChallenge.Api.Extensions.ServiceCollection;
using SC.DevChallenge.Configuration;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace SC.DevChallenge.Api
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration(Config)
                    .AddResponceCompression()
                    .AddCors()
                    .AddRouteOptions()
                    .AddCustomizedMvc()
                    .AddAutoMapper(typeof(Mapping.Mapper).Assembly)
                    .AddCustomizedSwagger();

            ApplicationContainer = services.AddAutofacAfter(Config);
            var provider = new AutofacServiceProvider(ApplicationContainer);

            return provider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) => 
            app.UseResponseCompression()
               .UseExceptionHandler(env)
               .UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
               .UseStaticFiles()
               .SetupSwagger()
               .UseAuthentication()
               .UseMvc();
    }
}
