using Newtonsoft.Json;
using Contracts;
using GiphyFetcher.Interfaces;
using GiphyFetcher.Models;
using Microsoft.Extensions.Caching.Distributed;
using GiphyFetcher.Caching;

namespace GiphyFetcher.Services
{
    public class SearchTermService : ISearchTermService
    {
        private readonly HttpClient _client;
        private readonly IDistributedCache _cache;
        private string giphyApiKey = string.Empty;
        private const string searchTermBaseUrl = "https://api.giphy.com/v1/gifs/search?";

        public SearchTermService(HttpClient client, IDistributedCache cache, IConfiguration config)
        {
            _client = client;
            _cache = cache;
            giphyApiKey = config.GetSection("GIPHY_API_KEY").Value;
        }

        public async Task<GifData> GetGifsBySearchTerm(SearchTermModel searchTermParams)
        {
            GetRequestParameters(searchTermParams,
                                             out string term,
                                             out int limit,
                                             out int offset,
                                             out string rating,
                                             out string language);

            // Check if the term already exists in cache,
            // if it is, return it instead of making a new call
            var cachedTerm = await DistributedInMemoryCache.GetTermFromCache(_cache, term);
            if (cachedTerm != null)
            {
                var cachedContent = JsonConvert.DeserializeObject<GifData>(cachedTerm);
                return cachedContent;
            }

            string giphyRequestUrl = BuildRequestUrl(term, limit, offset, rating, language);
            var response = await _client.GetAsync(giphyRequestUrl);
            
            var rawData = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<GifData>(rawData);

            if(content == null
                || (content.data.Length == 0 || content.meta.status != StatusCodes.Status200OK))
            {
                return new GifData();
            }

            // Cache the response from the API where the Key is the term and the Value is the response
            await DistributedInMemoryCache.AddTermToCache(_cache, term, rawData);

            return content;
        }

        #region Helper Methods
        private static void GetRequestParameters(SearchTermModel searchTerm,
                                                 out string term,
                                                 out int limit,
                                                 out int offset,
                                                 out string rating,
                                                 out string language)
        {
            term = searchTerm.Term;
            limit = searchTerm.Limit;
            offset = searchTerm.Offset;
            rating = searchTerm.Rating;
            language = searchTerm.Language;
        }

        private string BuildRequestUrl(string term, int limit, int offset, string rating, string language)
        {
            return $"{searchTermBaseUrl}" +
                                    $"api_key={giphyApiKey}" +
                                    $"&q={term}" +
                                    $"&limit={limit}" +
                                    $"&offset={offset}" +
                                    $"&rating={rating}" +
                                    $"&lang={language}";
        }
        #endregion
    }
}
