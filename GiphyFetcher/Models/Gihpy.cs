namespace GiphyFetcher.Models
{
    public class Gihpy
    {
        public string GIPHY_API_KEY { get; set; } = string.Empty;
    }

    public static class Ratings
    {
        public static string[] RatingTypes = new[] { "g", "pg", "pg13", "r", };
    }
}
