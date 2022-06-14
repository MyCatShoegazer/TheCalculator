using Microsoft.AspNetCore.Mvc;
using TheCalculator.Core.Services.Abstract;
using TheCalculator.Domain.ViewModels;
using TheCalculator.Domain.InputModels;

namespace TheCalculator.Api.Controllers;

/// <summary>
///     An API controller for math computing.
/// </summary>
[Route("computing")]
public class ComputingController : ControllerBase
{
    private readonly IComputingService _computingService;

    /// <summary>
    ///     Creates a new instance of computing controller.
    /// </summary>
    /// <param name="computingService">Computing service.</param>
    public ComputingController(IComputingService computingService)
    {
        _computingService = computingService;
    }

    /// <summary>
    ///     Computes square sums from provided chain in
    ///     <paramref name="squareChainInputModel" />.
    /// </summary>
    /// <param name="squareChainInputModel">
    ///     Values to compute square sums.
    /// </param>
    /// <returns>Returns computed square sums view model.</returns>
    [HttpPost]
    public async Task<ActionResult<SquareChainVm>> ComputeSquareChain(
        [FromBody] SquareChainInputModel squareChainInputModel)
    {
        var vm =
            await this._computingService.ComputeSquareChainSumAsync(
                squareChainInputModel);

        return vm;
    }
}
