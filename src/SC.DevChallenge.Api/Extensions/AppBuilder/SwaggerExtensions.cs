using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder SetupSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(SetSwaggerOptions);

            return app;
        }

        private static void SetSwaggerOptions(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "C# DevChallenge API");
            options.RoutePrefix = "api/docs";
        }
    }
}