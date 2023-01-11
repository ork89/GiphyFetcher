using GiphyFetcher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static GiphyFetcher.Models.Error;

namespace GiphyFetcher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            //if there are more then 1 error and all errors in the list are validation errors
            if(errors.All(e => e.Type == ErrorType.Validation))
            {
                // An object that holds binding and validation errors
                var modelStateDictionary = new ModelStateDictionary();

                foreach (var error in errors)
                {
                    modelStateDictionary.AddModelError(error.Title, error.Message);
                }

                return ValidationProblem(modelStateDictionary);
            }

            // if there's only 1 error
            var singleErorr = errors.FirstOrDefault();
            var statusCode = singleErorr?.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Failure => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: singleErorr?.Message);
        }
    }
}
