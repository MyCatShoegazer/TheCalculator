namespace TheCalculator.Domain.Infrastructure;

/// <summary>
///     A base model for API response models.
/// </summary>
public abstract class ApiModelBase
{
    /// <summary>
    ///     Response created UTC timestamp.
    /// </summary>
    public DateTimeOffset Created { get; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     Response signature.
    ///     <para>
    ///         Can be used to identify response in logs.
    ///     </para>
    /// </summary>
    public Guid Signature { get; } = Guid.NewGuid();
}
