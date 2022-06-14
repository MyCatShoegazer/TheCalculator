using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using TheCalculator.Domain.Infrastructure;

namespace TheCalculator.Core.Filters;

/// <summary>
///     Wraps unhandled exceptions to <see cref="ApiExceptionModel" />.
/// </summary>
public class ExceptionResponseFilter : ExceptionFilterAttribute
{
    private readonly IHostingEnvironment _environment;

    /// <summary>
    ///     Creates a new instance of exception response filter.
    /// </summary>
    /// <param name="environment">Application environment info.</param>
    public ExceptionResponseFilter(IHostingEnvironment environment)
    {
        _environment = environment;
    }

    /// <summary>
    ///     Occurs when unhandled exception fired.
    /// </summary>
    /// <param name="context">Exception context.</param>
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is not null)
        {
            context.Result =
                new BadRequestObjectResult(
                    new ApiExceptionModel(context.Exception,
                        this._environment.IsDevelopment()));
        }

        base.OnException(context);
    }
}
