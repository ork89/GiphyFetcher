using Contracts;
using GiphyFetcher.Interfaces;
using GiphyFetcher.Models;
using Newtonsoft.Json;

namespace GiphyFetcher.Services
{
    public class TrendingService : ITrendingService
    {
        private readonly HttpClient _client;
        private const string trendingBaseUrl = "https://api.giphy.com/v1/gifs/trending?";
        private string giphyApiKey = string.Empty;

        public TrendingService(HttpClient client, IConfiguration giphyApiKey)
        {
            _client = client;
            this.giphyApiKey = giphyApiKey.GetSection("GIPHY_API_KEY").Value;
        }

        public async Task<GifData> GetTrendingGifs(TrendingModel trendingParams)
        {
            var giphyTrendingRequestUrl = $"{trendingBaseUrl}" +
                                                $"api_key={giphyApiKey}" +
                                                $"&limit={trendingParams.Limit}" +
                                                $"&rating={trendingParams.Rating}";

            var response = await _client.GetAsync(giphyTrendingRequestUrl);

            if (response.IsSuccessStatusCode)
            {
                var rawData = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<GifData>(rawData);

                if (content != null
                    && content.data.Length > 0
                    && content.meta.status == StatusCodes.Status200OK)
                {
                    return content;
                }
            }

            return new GifData();
        }
    }
}
