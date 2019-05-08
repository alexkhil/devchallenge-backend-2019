using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SC.DevChallenge.Domain.Constants;

namespace SC.DevChallenge.Api.Infrastructure.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var dateStr = valueProviderResult.FirstValue;
            if (DateTime.TryParseExact(dateStr, DateFormat.Default, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"DateTime should be in format '{DateFormat.Default}'");
            return Task.CompletedTask;
        }
    }
}
