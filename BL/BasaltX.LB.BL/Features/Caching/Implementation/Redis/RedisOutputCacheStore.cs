using StackExchange.Redis;
using Microsoft.AspNetCore.OutputCaching;
using BasaltX.Utils.Features.Generics.Interfaces;
using System.Text;

namespace BasaltX.LB.BL.Features.Caching.Implementation.Redis;

/// <summary>
/// The redis output cache store.
/// </summary>
public class RedisOutputCacheStore : IOutputCacheStore
{
    #region Private members
    /// <summary>
    /// The connection multiplexer.
    /// </summary>
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    #endregion Private members

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RedisOutputCacheStore"/> class.
    /// </summary>
    /// <param name="connectionMultiplexer">The connection multiplexer.</param>
    public RedisOutputCacheStore(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }


    #endregion Constructors
    /// <summary>
    /// Evict by tag asynchronously.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="NotImplementedException"></exception>
    /// <returns>A ValueTask</returns>
    public async ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(tag);

        var redisDb = _connectionMultiplexer.GetDatabase();
        var cachedKeys = await redisDb.SetMembersAsync(tag);

        var keys = cachedKeys
            .Select(k => (RedisKey)k.ToString())
            .Concat(new[] { (RedisKey)tag })
            .ToArray();

        //Remove the data collection based on the keys
        await redisDb.KeyDeleteAsync(keys);
    }

    /// <summary>
    /// Get and return a valuetask of type byte[]? asynchronously.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="NotImplementedException"></exception>
    /// <returns><![CDATA[ValueTask<byte[]?>]]></returns>
    public async ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(key);

        var redisDb = _connectionMultiplexer.GetDatabase();

        return await redisDb.StringGetAsync(key);
    }

    /// <summary>
    /// Set data onto redis
    /// </summary>
    /// <param name="key">The key of the data store for later retrieval</param>
    /// <param name="value">The calue collection</param>
    /// <param name="tags"></param>
    /// <param name="validFor"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);

        var redisDb = _connectionMultiplexer.GetDatabase();

        foreach (string tag in tags ?? Array.Empty<string>())
        {
            //Associate the tag with the key of the collection
            await redisDb.SetAddAsync(tag, key);
        }
        await redisDb.StringSetAsync(key, value, validFor);
    }
}
