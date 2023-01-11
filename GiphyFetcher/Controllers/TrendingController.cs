using Contracts;
using GiphyFetcher.Interfaces;
using GiphyFetcher.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiphyFetcher.Controllers
{
    public class TrendingController : ApiBaseController
    {
        private readonly ITrendingService _trendingService;

        public TrendingController(ITrendingService trendingService)
        {
            _trendingService = trendingService;
        }

        [HttpPost]
        public async Task<IActionResult> GetTrendingGifs(CreateTrendingRequest request)
        {
            var trendingRequest = TrendingModel.From(request);
            var gifData = await _trendingService.GetTrendingGifs(trendingRequest);

            if (trendingRequest.Errors.Count > 0)
                return Problem(trendingRequest.Errors);

            return Ok(gifData);
        }
    }
}
