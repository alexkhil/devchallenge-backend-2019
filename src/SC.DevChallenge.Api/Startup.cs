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

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddConfiguration(Config)
                    .AddResponceCompression()
                    .AddCors()
                    .AddRouteOptions()
                    .AddCustomizedMvc()
                    .AddAutoMapper(typeof(Mapping.Mapper).Assembly)
                    .AddCustomizedSwagger();

        public static void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule(new AutofacModule());

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
