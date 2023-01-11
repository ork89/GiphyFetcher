using Microsoft.AspNetCore.Mvc;

namespace GiphyFetcher.Controllers
{
    // This attribute configuration excludes the error handler action from the app's OpenAPI specification
    // so there won't be a 500 error in OpenAPI/swagger page
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError() => Problem();
    }
}
