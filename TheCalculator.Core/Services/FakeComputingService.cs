using System.Runtime.CompilerServices;
using TheCalculator.Core.InputModels;
using TheCalculator.Core.Services.Abstract;
using TheCalculator.Core.ViewModels;

namespace TheCalculator.Core.Services;

/// <summary>
///     A fake computing service for testing purposes.
/// </summary>
public class FakeComputingService : IComputingService
{
    /// <inheritdoc />
    public ValueTask<double> ComputeSquareAsync(double value,
        CancellationToken cancellationToken = default)
    {
        const int SquarePower = 2;

        if (value <= 0d)
        {
            // TODO: localize.
            throw new ArgumentOutOfRangeException(nameof(value),
                "Provided value can't be less or equal to zero!");
        }

        cancellationToken.ThrowIfCancellationRequested();

        var result = Math.Pow(value, SquarePower);
        return ValueTask.FromResult(result);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<double> ComputeSquareChainAsync(
        IEnumerable<double> values,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var value in values)
            yield return
                await this.ComputeSquareAsync(value, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<SquareChainVm> ComputeSquareChainSumAsync(
        SquareChainInputModel squareChainInputModel,
        CancellationToken cancellationToken = default)
    {
        var sum = await this
            .ComputeSquareChainAsync(squareChainInputModel.Values,
                cancellationToken)
            .SumAsync(cancellationToken);

        return new SquareChainVm
        {
            Origin = squareChainInputModel.Values, Sum = sum
        };
    }
}
