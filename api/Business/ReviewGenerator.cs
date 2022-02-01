using review_api.Models;

namespace review_api.Business;

public class ReviewGenerator : IReviewGenerator
{
    private readonly IRatingGenerator _ratingGenerator;
    private readonly IMarkovChainTrainer _markovChainTrainer;

    public ReviewGenerator(IRatingGenerator ratingGenerator,
    IMarkovChainTrainer markovChainTrainer)
    {
        _ratingGenerator = ratingGenerator;
        _markovChainTrainer = markovChainTrainer;
    }

    public RandomReview GenerateRandomReview()
    {
        return new RandomReview {
            ReviewText = _markovChainTrainer.GetRandomReview(),
            Rating = _ratingGenerator.GenerateRandomRating()
        };
    }
}