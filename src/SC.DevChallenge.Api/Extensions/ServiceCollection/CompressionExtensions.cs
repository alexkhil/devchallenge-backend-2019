using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.DataAccess.EF;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class CompressionExtensions
    {
        public static IServiceCollection AddResponceCompression(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
            });

            return services;
        }
    }
}
