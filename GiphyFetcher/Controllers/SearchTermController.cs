using Contracts;
using GiphyFetcher.Interfaces;
using GiphyFetcher.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiphyFetcher.Controllers
{
    public class SearchTermController : ApiBaseController
    {
        private readonly ISearchTermService _searchTermService;

        public SearchTermController(ISearchTermService searchTermService)
        {
            _searchTermService = searchTermService;
        }

        [HttpPost]
        public async Task<IActionResult> GetGifsBySearch(CreateSearchTermRequest request)
        {
            var searchRequest = SearchTermModel.From(request);
            var gifDataObj = await _searchTermService.GetGifsBySearchTerm(searchRequest);

            if (searchRequest.Errors.Count > 0)
                return Problem(searchRequest.Errors);

            return Ok(gifDataObj);
        }
    }
}
