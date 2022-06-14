using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using TheCalculator.Core.Extensions.Types;

namespace TheCalculator.Core.Extensions;

/// <summary>
///     Provides set of extensions for <see cref="IDistributedCache" />.
/// </summary>
public static class DistributedCacheExtensions
{
    private const int DefaultAbsoluteExpirationSeconds = 600;
    private const int DefaultSlidingExpirationSeconds = 300;

    /// <summary>
    ///     Stores default cache entry options.
    /// </summary>
    private static DistributedCacheEntryOptions DefaultOptions
    {
        get
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(
                TimeSpan.FromSeconds(DefaultAbsoluteExpirationSeconds));
            options.SetSlidingExpiration(
                TimeSpan.FromSeconds(DefaultSlidingExpirationSeconds));

            return options;
        }
    }

    /// <summary>
    ///     Gets value by provided <paramref name="key" />
    ///     from distributed cache. If requested value doesn't exists,
    ///     it will be created automatically from provided
    ///     <paramref name="aquire" /> function.
    /// </summary>
    /// <param name="cache">Distributed cache instance.</param>
    /// <param name="key">Key in cache.</param>
    /// <param name="aquire">
    ///     Accessor function to get new values if they aren't in cache.
    /// </param>
    /// <param name="options">Cache entry options.</param>
    /// <param name="cancellationToken">
    ///     Propagates notification that operations should be canceled.
    /// </param>
    /// <typeparam name="TValue">A type of value to cache.</typeparam>
    /// <returns>
    ///     Returns cached value of type <typeparamref name="TValue" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurs when aquire value is null or value got from cache is null.
    /// </exception>
    public static async Task<TValue> GetOrSetAsync<TValue>(
        this IDistributedCache cache, string key,
        Func<ValueTask<TValue>> aquire,
        DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= DefaultOptions;

        var value = await cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(value))
        {
            var newValue = await aquire();
            if (newValue is null)
            {
                throw new InvalidOperationException(
                    "Aquire value can't be null!");
            }

            var entry = new CacheEntry<TValue>(newValue);
            var serializedNewValue = JsonSerializer.Serialize(entry);
            await cache.SetStringAsync(key, serializedNewValue, options,
                cancellationToken);

            return newValue;
        }

        var deserializedValue =
            JsonSerializer.Deserialize<CacheEntry<TValue>>(value);
        if (deserializedValue is null)
        {
            throw new InvalidOperationException("Cache reading error!");
        }

        return deserializedValue.Payload;
    }

    /// <summary>
    ///     Gets a new cache key.
    /// </summary>
    /// <param name="cache">Distributed cache instance.</param>
    /// <param name="body">Cache key body.</param>
    /// <param name="prefix">
    ///     Cache key prefix.
    ///     <para>
    ///         By default, it will be equal to the name of calling member.
    ///     </para>
    /// </param>
    /// <param name="postfix">Cache key postfix.</param>
    /// <returns>Returns generated cache key.</returns>
    public static string MakeKey(this IDistributedCache cache, string body,
        [CallerMemberName] string? prefix = "", string? postfix = null)
    {
        const string Delimiter = "_";
        return $"{prefix}{Delimiter}{body}{Delimiter}{postfix}";
    }
}
