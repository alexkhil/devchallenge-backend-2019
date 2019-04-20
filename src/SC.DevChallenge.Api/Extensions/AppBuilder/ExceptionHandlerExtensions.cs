using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SC.DevChallenge.Api.Infrastructure;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(
            this IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }

            return app;
        }
    }
}