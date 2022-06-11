using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;
using TheCalculator.Core.InputModels;
using TheCalculator.Core.Options;
using TheCalculator.Core.Services.Abstract;
using TheCalculator.Core.ViewModels;

namespace TheCalculator.Core.Services;

/// <summary>
///     A fake computing service for testing purposes.
/// </summary>
public class FakeComputingService : IComputingService
{
    private readonly ComputingOptions _computingOptions;

    /// <summary>
    ///     Creates a new instance of fake computing service.
    /// </summary>
    /// <param name="computingOptions">Computing options.</param>
    public FakeComputingService(IOptions<ComputingOptions> computingOptions)
    {
        this._computingOptions = computingOptions.Value;
    }

    /// <inheritdoc />
    public async ValueTask<double> ComputeSquareAsync(double value,
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

        await Task.Delay(GetFakeDelay(this._computingOptions),
            cancellationToken);

        var result = Math.Pow(value, SquarePower);
        return result;
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<double> ComputeSquareChainAsync(
        IEnumerable<double> values,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var value in values)
            yield return
                // TODO: add caching here
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

    /// <summary>
    ///     Gets fake delay in milliseconds.
    /// </summary>
    /// <param name="computingOptions">Computing options.</param>
    /// <returns>Returns fake delay in millisecond.</returns>
    private static int GetFakeDelay(ComputingOptions computingOptions)
    {
        var random = new Random();
        return random.Next(computingOptions.FakeSquareDelayMin,
            computingOptions.FakeSquareDelayMax);
    }
}
