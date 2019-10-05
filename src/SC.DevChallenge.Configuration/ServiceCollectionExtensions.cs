using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
                .ConfigureExplicit<DbConfiguration>(config.GetSection(ConfigurationPaths.DbMain));

            return services;
        }

        private static IServiceCollection ConfigureExplicit<TOption>(
            this IServiceCollection services,
            IConfigurationSection section)
            where TOption : class, new() =>
            services
                .Configure<TOption>(section)
                .AddTransient(x => x.GetService<IOptionsSnapshot<TOption>>().Value);
    }
}