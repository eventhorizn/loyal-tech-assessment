namespace review_api.Business;

public interface IRatingGenerator
{
    int GenerateRandomRating();
    int GenerateSentimentRating(string reviewText);
}