using Microsoft.AspNetCore.Mvc;

namespace GiphyFetcher.Models
{
    public class Error
    {
        public ErrorType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;


        public enum ErrorType {
            Validation,
            Unexpected,
            Binding,
            NotFound,
            Failure
        }
    }
}
