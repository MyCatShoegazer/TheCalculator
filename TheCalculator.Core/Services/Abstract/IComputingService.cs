using TheCalculator.Core.InputModels;
using TheCalculator.Core.ViewModels;

namespace TheCalculator.Core.Services.Abstract;

/// <summary>
///     An interface for computing services.
/// </summary>
public interface IComputingService
{
    /// <summary>
    ///     Computes square for provided <paramref name="value" />.
    /// </summary>
    /// <param name="value">Value to compute square for.</param>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>
    ///     Returns computes <paramref name="value" /> square.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Will be caused if <paramref name="value" />
    ///     is less or equal to zero.
    /// </exception>
    public ValueTask<double> ComputeSquareAsync(double value,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Computes squares for items in provided
    ///     <paramref name="values" /> sequence.
    /// </summary>
    /// <param name="values">Sequence to compute it's items squares.</param>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>Returns same sequence with computes item's squares.</returns>
    public IAsyncEnumerable<double> ComputeSquareChainAsync(
        IEnumerable<double> values,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Computes square sums provided
    ///     <paramref name="squareChainInputModel" /> values.
    /// </summary>
    /// <param name="squareChainInputModel">
    ///     Values to compute their square.
    /// </param>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>Returns computes chain square sums view model.</returns>
    public ValueTask<SquareChainVm> ComputeSquareChainSumAsync(
        SquareChainInputModel squareChainInputModel,
        CancellationToken cancellationToken = default);
}
