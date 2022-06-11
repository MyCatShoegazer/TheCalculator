namespace TheCalculator.Core.Options;

/// <summary>
///     Computing options.
/// </summary>
public class ComputingOptions
{
    /// <summary>
    ///     Fake square computing min delay in milliseconds.
    /// </summary>
    public int FakeSquareDelayMin { get; set; } = 100;

    /// <summary>
    ///     Fake square computing max delay in milliseconds.
    /// </summary>
    public int FakeSquareDelayMax { get; set; } = 500;
}
