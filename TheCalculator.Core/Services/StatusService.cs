using AutoMapper;
using Microsoft.Extensions.Options;
using TheCalculator.Core.Options;
using TheCalculator.Core.Services.Abstract;
using TheCalculator.Core.ViewModels;

namespace TheCalculator.Core.Services;

/// <summary>
///     General application status service.
/// </summary>
public class StatusService : IStatusService
{
    private readonly VersionOptions _versionOptions;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Creates a new instance of application status service.
    /// </summary>
    /// <param name="versionOptions">Application version info.</param>
    /// <param name="mapper">Object mapper.</param>
    public StatusService(IOptions<VersionOptions> versionOptions,
        IMapper mapper)
    {
        this._versionOptions = versionOptions.Value;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public ValueTask<VersionVm> GetVersionAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var vm =
            this._mapper.Map<VersionOptions, VersionVm>(this._versionOptions);

        return ValueTask.FromResult(vm);
    }

    /// <inheritdoc />
    public ValueTask<bool> GetIsAliveAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return ValueTask.FromResult(true);
    }
}
