using ProjectName.Application.Common.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Extensions;
public static class DistributedCacheExtension
{
    public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default(CancellationToken))
    {
        Throw.Exception.IfNull(distributedCache, "distributedCache");
        Throw.Exception.IfNull(cacheKey, "cacheKey");
        byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(continueOnCapturedContext: false);
        if (utf8Bytes != null)
        {
            return JsonSerializer.Deserialize<T>(utf8Bytes);
        }

        return default(T);
    }

    public static async Task RemoveAsync(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default(CancellationToken))
    {
        Throw.Exception.IfNull(distributedCache, "distributedCache");
        Throw.Exception.IfNull(cacheKey, "cacheKey");
        await distributedCache.RemoveAsync(cacheKey, token).ConfigureAwait(continueOnCapturedContext: false);
    }

    public static async Task SetAsync<T>(this IDistributedCache distributedCache, string cacheKey, T obj, int cacheExpirationInMinutes = 30, CancellationToken token = default(CancellationToken))
    {
        Throw.Exception.IfNull(distributedCache, "distributedCache");
        Throw.Exception.IfNull(cacheKey, "cacheKey");
        Throw.Exception.IfNull(obj, "obj");
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(cacheExpirationInMinutes));
        byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
        await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(continueOnCapturedContext: false);
    }
}
