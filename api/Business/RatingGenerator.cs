using System.Security.Cryptography;

namespace review_api.Business;

public class RatingGenerator(IAzureAITextAnalytics azureAITextAnalytics) : IRatingGenerator
{
    private readonly IAzureAITextAnalytics _azureAITextAnalytics = azureAITextAnalytics;

    public int GenerateRandomRating()
    {
        return RandomNumberGenerator.GetInt32(1, 6);
    }

    public int GenerateSentimentRating(string reviewText)
    {
        return _azureAITextAnalytics.GetSentimentAnalysis(reviewText);
    }
}