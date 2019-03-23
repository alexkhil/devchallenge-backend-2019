using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class SwaggerExtensions
    {
        private const string SwaggerEndpoint = "/swagger/v1/swagger.json";

        public static IApplicationBuilder SetupSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger()
               .UseSwaggerUI(SetSwaggerOptions);

            return app;
        }

        private static void SetSwaggerOptions(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint(SwaggerEndpoint, "C# DevChallenge API");
            options.RoutePrefix = string.Empty;
        }
    }
}