namespace TheCalculator.Core.Options;

/// <summary>
///     Application version info.
/// </summary>
public class VersionOptions
{
    /// <summary>
    ///     Version number.
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    ///     Build number.
    /// </summary>
    public string? Build { get; set; }

    /// <summary>
    ///     VCS branch name.
    /// </summary>
    public string? Branch { get; set; }
}
