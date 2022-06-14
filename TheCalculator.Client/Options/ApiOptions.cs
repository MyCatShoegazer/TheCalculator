namespace TheCalculator.Client.Options;

/// <summary>
///     An API options of application.
/// </summary>
public class ApiOptions
{
    public string? ApiBasePath { get; set; }

    public RoutesOptions? Routes { get; set; }
}

