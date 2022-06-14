using TheCalculator.Client.Options;

namespace TheCalculator.Client.Services.Abstract;

/// <summary>
///     An interface for option providers.
/// </summary>
public interface IOptionsProviderService
{
    /// <summary>
    ///     Gets application API options from configuration file.
    /// </summary>
    /// <returns>Returns application API options.</returns>
    public ApiOptions GetApiOptions();
}
