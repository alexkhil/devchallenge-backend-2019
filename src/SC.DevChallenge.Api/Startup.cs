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
            services.AddConfiguration(Config);
            services.AddResponceCompression();
            services.AddCors();
            services.AddRouteOptions();

            services.AddCustomizedMvc();

            services.AddAutoMapper(typeof(Mapping.Mapper).Assembly);

            services.AddCustomizedSwagger();

            ApplicationContainer = services.AddAutofacAfter(Config);
            var provider = new AutofacServiceProvider(ApplicationContainer);

            return provider;
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime)
        {
            app.UseResponseCompression();
            app.UseExceptionHandler(env);

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.SetupSwagger();

            app.UseAuthentication();

            app.UseMvc();

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
