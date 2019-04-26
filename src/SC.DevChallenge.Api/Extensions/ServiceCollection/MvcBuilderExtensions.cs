using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SC.DevChallenge.Domain.Date;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomJsonOptions(this IMvcBuilder mvcBuilder) =>
            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = DateTimeFormat.Default;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    CamelCaseText = true
                });
            });
    }
}
