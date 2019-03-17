using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Configuration.DataAccess;

namespace SC.DevChallenge.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services
                .AddOptions()
                .Configure<DbConfiguration>(config.GetSection(ConfigurationPaths.DbMain));

            return services;
        }
    }
}