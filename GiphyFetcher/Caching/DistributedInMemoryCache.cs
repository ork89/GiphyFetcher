using Microsoft.Extensions.Caching.Distributed;

namespace GiphyFetcher.Caching
{
    public static class DistributedInMemoryCache
    {
        // In a real world application I might have wanted to implement more
        // types of caching like SQL Caching and distributed caching via Redis.
        public static async Task AddTermToCache(IDistributedCache cache, string term, string responseContent)
        {
            // provide a absolute value for the lifespan of the object in cache
            DistributedCacheEntryOptions options = new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };

            await DistributedCacheExtensions.SetStringAsync(cache, term, responseContent, options);
        }

        public static async Task<string> GetTermFromCache(IDistributedCache cache, string term)
        {
            return await DistributedCacheExtensions.GetStringAsync(cache, term);
        }
    }
}
