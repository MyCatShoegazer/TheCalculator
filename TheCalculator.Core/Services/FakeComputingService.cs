using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using TheCalculator.Core.Extensions;
using TheCalculator.Core.InputModels;
using TheCalculator.Core.Options;
using TheCalculator.Core.Options.Features;
using TheCalculator.Core.Services.Abstract;
using TheCalculator.ViewModels;

namespace TheCalculator.Core.Services;

/// <summary>
///     A fake computing service for testing purposes.
/// </summary>
public class FakeComputingService : IComputingService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IFeatureManager _featureManager;
    private readonly ComputingOptions _computingOptions;

    /// <summary>
    ///     Creates a new instance of fake computing service.
    /// </summary>
    /// <param name="computingOptions">Computing options.</param>
    /// <param name="distributedCache">Distributed cache.</param>
    /// <param name="featureManager">Feature manager.</param>
    public FakeComputingService(IOptions<ComputingOptions> computingOptions,
        IDistributedCache distributedCache, IFeatureManager featureManager)
    {
        _distributedCache = distributedCache;
        _featureManager = featureManager;
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
        var cacheEnabled =
            await this._featureManager.IsEnabledAsync(FeatureFlags.UseCache);

        foreach (var value in values)
        {
            if (!cacheEnabled)
                yield return await this.ComputeSquareAsync(value,
                    cancellationToken);

            var key =
                this._distributedCache.MakeKey(
                    value.ToString(CultureInfo.InvariantCulture));

            var cacheValue = await this._distributedCache.GetOrSetAsync(key,
                async () =>
                    await this.ComputeSquareAsync(value, cancellationToken),
                cancellationToken: cancellationToken);

            yield return cacheValue;
        }
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
