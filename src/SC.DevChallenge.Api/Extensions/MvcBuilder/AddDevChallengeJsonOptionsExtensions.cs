using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SC.DevChallenge.Domain.Constants;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class AddDevChallengeJsonOptionsExtensions
    {
        public static IMvcBuilder AddDevChallengeJsonOptions(this IMvcBuilder mvcBuilder) =>
            mvcBuilder.AddJsonOptions(SetupMvcJsonOptions);

        private static void SetupMvcJsonOptions(MvcJsonOptions options)
        {
            options.SerializerSettings.DateFormatString = DateFormat.Default;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter
            {
                CamelCaseText = true
            });
        }
    }
}
