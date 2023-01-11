namespace GiphyFetcher.Models
{
    public class TrendingModel
    {
        // Giphy's public API Key limits the amount of gifs
        // you can get in a single request to 50 gifs.
        private const int MaxLimitNumber = 50;

        public int Limit { get; }
        public string Rating { get; }

        public List<Error> Errors { get; }

        private TrendingModel(int limit, string rating, List<Error> errors)
        {
            Limit = limit;
            Rating = rating;
            Errors = errors;
        }

        public static TrendingModel CreateTrending(int limit = 1, string rating = "g")
        {
            List<Error> errors = new();

            if(limit is < 1 or > MaxLimitNumber)
            {
                errors.Add(new Error()
                {
                    Type = Error.ErrorType.Validation,
                    Title = "Trending.LimitOutOfRange",
                    Message = $"The amount of gifs in the request should be between 1 and {MaxLimitNumber}"
                });
            }

            if(!Ratings.RatingTypes.Contains(rating))
            {
                errors.Add(new Error()
                {
                    Type = Error.ErrorType.Validation,
                    Title = "Trending.RatingNotFound",
                    Message = $"Acceptable ratings are: {string.Join(", ", Ratings.RatingTypes)}"
                });
            }

            return new TrendingModel(limit, rating, errors);
        }
    }
}
