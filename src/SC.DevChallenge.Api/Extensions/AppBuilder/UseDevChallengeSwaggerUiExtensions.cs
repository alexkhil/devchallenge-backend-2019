using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class UseDevChallengeSwaggerUiExtensions
    {
        private const string RoutePrefix = "api/docs";
        private const string SwaggerEndpoint = "/swagger/v1/swagger.json";

        public static IApplicationBuilder UseDevChallengeSwaggerUi(this IApplicationBuilder appBuilder) =>
            appBuilder.UseSwaggerUI(SetupSwaggerUiOptions);

        private static void SetupSwaggerUiOptions(SwaggerUIOptions options)
        {
            options.DisplayRequestDuration();
            options.DocumentTitle = typeof(Startup).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
            options.SwaggerEndpoint(SwaggerEndpoint, "v1");
            options.RoutePrefix = RoutePrefix;
        }
    }
}