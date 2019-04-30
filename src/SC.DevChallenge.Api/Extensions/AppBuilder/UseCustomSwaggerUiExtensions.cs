using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class UseCustomSwaggerUiExtensions
    {
        private const string RoutePrefix = "api/docs";
        private const string SwaggerEndpoint = "/swagger/v1/swagger.json";

        public static IApplicationBuilder UseCustomSwaggerUi(this IApplicationBuilder appBuilder) =>
            appBuilder.UseSwaggerUI(
                options =>
                {
                    options.DisplayRequestDuration();
                    options.DocumentTitle = typeof(Startup).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    options.SwaggerEndpoint(SwaggerEndpoint, "v1");
                    options.RoutePrefix = RoutePrefix;
                });
    }
}