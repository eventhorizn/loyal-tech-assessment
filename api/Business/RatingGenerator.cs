using System.Security.Cryptography;

namespace review_api.Business;

public class RatingGenerator : IRatingGenerator
{
    private readonly IAzureAITextAnalytics _azureAITextAnalytics;

    public RatingGenerator(IAzureAITextAnalytics azureAITextAnalytics)
    {
        _azureAITextAnalytics = azureAITextAnalytics;
    }

    public int GenerateRandomRating()
    {
        return RandomNumberGenerator.GetInt32(1, 6);
    }

    public int GenerateSentimentRating(string reviewText)
    {
        return _azureAITextAnalytics.GetSentimentAnalysis(reviewText);
    }
}