using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeCompression(this IServiceCollection services)
        {
            var compressionLevel = CompressionLevel.Optimal;

            return services.Configure<BrotliCompressionProviderOptions>(options => options.Level = compressionLevel)
                .Configure<GzipCompressionProviderOptions>(options => options.Level = compressionLevel)
                .AddResponseCompression(SetupResponseCompressionOptions);

            static void SetupResponseCompressionOptions(ResponseCompressionOptions options)
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            }
        }
    }
}
