using TheCalculator.Domain.InputModels;
using TheCalculator.Domain.ViewModels;

namespace TheCalculator.Client.Services.Abstract;

/// <summary>
///     An interface for HTTP computing services.
/// </summary>
public interface IHttpComputingService
{
    /// <summary>
    ///     Gets computes squares from API.
    /// </summary>
    /// <param name="squareChainInputModel">Squares to compute.</param>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>Returns computes squares.</returns>
    public Task<SquareChainVm?> GetComputedSquaresAsync(
        SquareChainInputModel squareChainInputModel,
        CancellationToken cancellationToken = default);
}
