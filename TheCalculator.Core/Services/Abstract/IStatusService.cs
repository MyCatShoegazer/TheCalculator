using TheCalculator.Core.ViewModels;

namespace TheCalculator.Core.Services.Abstract;

/// <summary>
///     An interface for status services.
/// </summary>
public interface IStatusService
{
    /// <summary>
    ///     Gets application version.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>Returns version view model.</returns>
    public ValueTask<VersionVm> GetVersionAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets application alive status.
    ///     <para>
    ///         Use this as check value
    ///         for container environments.
    ///     </para>
    /// </summary>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <returns>Returns <see langword="true" />.</returns>
    public ValueTask<bool> GetIsAliveAsync(
        CancellationToken cancellationToken = default);
}
