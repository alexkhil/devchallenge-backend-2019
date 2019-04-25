using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class CompressionExtensions
    {
        private const CompressionLevel compressionLevel = CompressionLevel.Optimal;

        public static IServiceCollection AddResponceCompression(this IServiceCollection services) =>
            services
            .Configure<BrotliCompressionProviderOptions>(options => options.Level = compressionLevel)
            .Configure<GzipCompressionProviderOptions>(options => options.Level = compressionLevel)
            .AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
    }
}
