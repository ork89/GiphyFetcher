using Contracts;
using GiphyFetcher.Models;

namespace GiphyFetcher.Interfaces
{
    public interface ISearchTermService
    {
        Task<GifData> GetGifsBySearchTerm(SearchTermModel searchTermParams);
    }
}
