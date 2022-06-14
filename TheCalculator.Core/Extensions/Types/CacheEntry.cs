namespace TheCalculator.Core.Extensions.Types;

/// <summary>
///     Cache entry with timestamp.
///     <para>
///         Used to encapsulate complex and primitive types.
///     </para>
/// </summary>
/// <typeparam name="TValue">Payload value type.</typeparam>
public class CacheEntry<TValue>
{
    public CacheEntry(TValue payload)
    {
        this.Payload = payload;
    }

    public TValue Payload { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}
