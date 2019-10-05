using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Filters;
using SC.DevChallenge.Api.Infrastructure.ModelBinders;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class AddDevChallengeMvcExtensions
    {
        public static IServiceCollection AddDevChallengeMvc(this IServiceCollection services) =>
            services.AddMvc(SetupMvcOptions)
            .AddDevChallengeJsonOptions()
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .Services;

        private static void SetupMvcOptions(MvcOptions options)
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));

            options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
        }
    }
}