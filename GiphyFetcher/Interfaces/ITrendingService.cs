using Contracts;
using GiphyFetcher.Models;

namespace GiphyFetcher.Interfaces
{
    public interface ITrendingService
    {
        Task<GifData> GetTrendingGifs(TrendingModel trendingParams);
    }
}
