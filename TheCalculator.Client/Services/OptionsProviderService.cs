using TheCalculator.Client.Options;
using TheCalculator.Client.Services.Abstract;

namespace TheCalculator.Client.Services;

/// <summary>
///     General option provider.
/// </summary>
public class OptionsProviderService : IOptionsProviderService
{
    private const string ConfigurationPath = "ApiOptions";

    private readonly IConfiguration _configuration;

    /// <summary>
    ///     Creates a new instance of option provider.
    /// </summary>
    /// <param name="configuration">Current application configuration.</param>
    public OptionsProviderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public ApiOptions GetApiOptions()
    {
        var options = new ApiOptions();
        this._configuration.Bind(ConfigurationPath, options);
        return options;
    }
}
