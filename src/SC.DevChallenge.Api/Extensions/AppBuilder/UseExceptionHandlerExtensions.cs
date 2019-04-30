using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SC.DevChallenge.Api.Infrastructure;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class UseExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, IHostingEnvironment env) =>
            env.IsDevelopment()
            ? app.UseDeveloperExceptionPage()
                 .UseDatabaseErrorPage()
            : app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}