using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Configuration;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class AddDevChallengeCorsExtensions
    {
        private static readonly Dictionary<string, Action<CorsPolicyBuilder>> corsPolicies = new Dictionary<string, Action<CorsPolicyBuilder>>
        {
            [CorsPolicyName.AllowAny] = x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        };

        public static IServiceCollection AddDevChallengeCors(this IServiceCollection services) =>
            services.AddCors(SetupCorsOptions);

        private static void SetupCorsOptions(CorsOptions options)
        {
            foreach (var policy in corsPolicies)
            {
                options.AddPolicy(policy.Key, policy.Value);
            }
        }
    }
}
