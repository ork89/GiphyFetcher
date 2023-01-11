using Contracts;

namespace GiphyFetcher.Models
{
    public class SearchTermModel
    {
        public const int MinTermLength = 3;
        public const int MaxTermLength = 25;

        public string Term { get; }
        public int Limit { get; }
        public int Offset { get; }
        public string Rating { get; }
        public string Language { get; }

        public List<Error> Errors { get; }

        private SearchTermModel(string term, int limit, int offset, string rating, string language, List<Error> errors)
        {
            Term = term;
            Limit = limit;
            Offset = offset;
            Rating = rating;
            Language = language;
            Errors = errors;
        }

        public static SearchTermModel CreateSearchTerm(string term, int limit = 1, int offset = 0, string rating = "g", string langauge = "en")
        {
            // implement internal/business logic validations
            // (these are just some of the validations we can implement)

            List<Error> errors = new();

            if(string.IsNullOrEmpty(term) || string.IsNullOrWhiteSpace(term))
            {
                errors.Add(new Error()
                {
                    Type = Error.ErrorType.Validation,
                    Title = "SearchTerm.InvalidTerm",
                    Message = "A valid search term is required"
                });
            }

            if(term.Length is < MinTermLength or > MaxTermLength)
            {
                errors.Add(new Error()
                {
                    Type = Error.ErrorType.Validation,
                    Title = "SearchTerm.InvalidTermLength",
                    Message = $"A valid search term should be between {MinTermLength} and {MaxTermLength} characters long"
                });
            }

            if(!Ratings.RatingTypes.Contains(rating))
            {
                errors.Add(new Error()
                    {
                        Type = Error.ErrorType.Validation,
                        Title = "SearchTerm.RatingNotFound",
                        Message = $"Acceptable ratings are: {string.Join(", ", Ratings.RatingTypes)}"
                }
                );
            }

            return new SearchTermModel(term, limit, offset, rating, langauge, errors);
        }

        public static SearchTermModel From(CreateSearchTermRequest request)
        {
            return CreateSearchTerm(request.Term, request.Limit, request.Offset, request.Rating, request.Language);
        }
    }
}
