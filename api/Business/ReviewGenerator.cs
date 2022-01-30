public class ReviewGenerator : IReviewGenerator
{
    public RandomReview GenerateRandomReview()
    {
        return new RandomReview {
            ReviewText = "Test review",
            Rating = 3
        };
    }

    private int GenerateRating()
    {
        return 0;

    }
}