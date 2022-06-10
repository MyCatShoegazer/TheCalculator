using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TheCalculator.Core.Infrastructure;

namespace TheCalculator.Core.Filters;

/// <summary>
///     Wraps normal responses to <see cref="ApiResponseModel{T}" />.
/// </summary>
public class ActionResponseFilter : ActionFilterAttribute
{
    /// <summary>
    ///     Occurs on route executed action.
    /// </summary>
    /// <param name="context">Execution context.</param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            context.Result =
                new ObjectResult(
                    new ApiResponseModel<object>(objectResult.Value));
        }

        base.OnActionExecuted(context);
    }
}
