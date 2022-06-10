using Microsoft.AspNetCore.Mvc;
using TheCalculator.Core.Filters;

namespace TheCalculator.Api.Controllers;

/// <summary>
///     A base API controller from which others should be inherited.
/// </summary>
[ApiController]
[ServiceFilter(typeof(ExceptionResponseFilter))]
[ServiceFilter(typeof(ActionResponseFilter))]
public abstract class ControllerBase
{ }
