using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SC.DevChallenge.Api.Infrastructure.ModelBinders
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return IsDateTime(context.Metadata.ModelType)
                ? new DateTimeModelBinder()
                : null;
        }

        private static bool IsDateTime(Type modelType) =>
            modelType == typeof(DateTime) || modelType == typeof(DateTime?);
    }
}
