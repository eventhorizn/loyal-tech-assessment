using review_api.Models;

namespace review_api.Business;

public class ReviewGenerator(IRatingGenerator ratingGenerator,
    IMarkovChainTrainer markovChainTrainer,
    IConfiguration configuration) : IReviewGenerator
{
    private readonly IRatingGenerator _ratingGenerator = ratingGenerator;
    private readonly IMarkovChainTrainer _markovChainTrainer = markovChainTrainer;

    private readonly bool _useSentimentAnalysis = bool.Parse(configuration.
            GetSection("SentimentAnalysis").
            GetSection("UseSentimentAnalysis").Value);

    public RandomReview GenerateRandomReview()
    {
        var reviewText = _markovChainTrainer.GetRandomReview();
        var rating = _useSentimentAnalysis ? 
            _ratingGenerator.GenerateSentimentRating(reviewText) : 
            _ratingGenerator.GenerateRandomRating();

        return new RandomReview {
            ReviewText = reviewText,
            Rating = rating
        };
    }
}