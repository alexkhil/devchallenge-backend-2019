using System.Reflection;
using Microsoft.AspNetCore.Builder;
using SC.DevChallenge.Api.Constants;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SC.DevChallenge.Api.Extensions.ApplicationBuilder
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDevChallengeSwaggerUi(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseSwaggerUI(SetupSwaggerUiOptions);

            static void SetupSwaggerUiOptions(SwaggerUIOptions options)
            {
                options.DisplayRequestDuration();
                options.DocumentTitle = typeof(Startup).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                options.SwaggerEndpoint($"/swagger/{ApiVersions.V1}/swagger.json", ApiVersions.V1);
                options.RoutePrefix = "api/docs";
            }
        }
    }
}