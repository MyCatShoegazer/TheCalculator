using Microsoft.AspNetCore.Mvc;
using TheCalculator.Core.Services.Abstract;
using TheCalculator.Core.ViewModels;

namespace TheCalculator.Api.Controllers;

/// <summary>
///     An API controller which provides application status information.
/// </summary>
[Route("status")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    /// <summary>
    ///     Creates a new instance of status controller.
    /// </summary>
    /// <param name="statusService">Status service.</param>
    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    /// <summary>
    ///     Gets application version.
    /// </summary>
    /// <returns>Returns version view model.</returns>
    [HttpGet]
    public async Task<ActionResult<VersionVm>> GetVersion()
    {
        var vm = await this._statusService.GetVersionAsync();
        return vm;
    }

    /// <summary>
    ///     Gets application alive status.
    ///     <para>
    ///         It always returns <see langword="true" />
    ///         representing running application.
    ///         If you got something else then
    ///         probably the bad things caused.
    ///     </para>
    /// </summary>
    /// <returns>Returns <see langword="true" />.</returns>
    [HttpGet("health/alive")]
    public async Task<ActionResult<bool>> GetIsAliveAsync()
    {
        var result = await this._statusService.GetIsAliveAsync();
        return result;
    }
}
