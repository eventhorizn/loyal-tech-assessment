public class ReviewGenerator : IReviewGenerator
{
    private readonly IRatingGenerator _ratingGenerator;

    public ReviewGenerator(IRatingGenerator ratingGenerator)
    {
        _ratingGenerator = ratingGenerator;
    }

    public RandomReview GenerateRandomReview()
    {
        return new RandomReview {
            ReviewText = "Test review",
            Rating = _ratingGenerator.GenerateRandomRating()
        };
    }
}