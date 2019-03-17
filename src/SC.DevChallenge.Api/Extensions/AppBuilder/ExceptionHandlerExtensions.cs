using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace SC.DevChallenge.Api.Extensions.AppBuilder
{
    public static class ExceptionHandlerExtensions
    {
        private const string PagePath = "/Error/{0}";

        public static void UseExceptionHandler(
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
                app.UseStatusCodePagesWithReExecute(PagePath);
            }
        }
    }
}