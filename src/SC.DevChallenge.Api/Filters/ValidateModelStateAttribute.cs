using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SC.DevChallenge.Api.Filters
{
    public class ValidateModelStateAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
