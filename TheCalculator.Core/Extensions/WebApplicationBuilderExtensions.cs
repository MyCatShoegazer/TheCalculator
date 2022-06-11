using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TheCalculator.Core.Filters;
using TheCalculator.Core.Options;
using TheCalculator.Core.Services;
using TheCalculator.Core.Services.Abstract;

namespace TheCalculator.Core.Extensions;

/// <summary>
///     Contains application startup scripts.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Configures application options.
    /// </summary>
    /// <param name="builder">Application builder to configure with.</param>
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<VersionOptions>(
            builder.Configuration.GetSection("Version"));

        builder.Services.Configure<ComputingOptions>(
            builder.Configuration.GetSection("Computing"));
    }

    /// <summary>
    ///     Configures object mapper.
    /// </summary>
    /// <param name="builder">Application builder to configure with.</param>
    public static void ConfigureAutomapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(x => x.AddMaps("TheCalculator.Core"));
    }

    /// <summary>
    ///     Configures API services.
    /// </summary>
    /// <param name="builder">Application builder to configure with.</param>
    public static void AddApiServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStatusService, StatusService>();
        builder.Services.AddScoped<IComputingService, FakeComputingService>();
    }

    /// <summary>
    ///     Configures API filters.
    /// </summary>
    /// <param name="builder">Application builder to configure with.</param>
    public static void AddApiFilters(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ExceptionResponseFilter>();
        builder.Services.AddScoped<ActionResponseFilter>();
    }
}
