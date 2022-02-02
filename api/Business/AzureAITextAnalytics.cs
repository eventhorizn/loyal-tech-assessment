using Azure;
using Azure.AI.TextAnalytics;

namespace review_api.Business;

public class AzureAITextAnalytics : IAzureAITextAnalytics
{
    private readonly IConfiguration _configuration;
    private readonly AzureKeyCredential _credentials;
    private readonly Uri _endpoint;

    public AzureAITextAnalytics(IConfiguration configuration)
    {
        _configuration = configuration;
        _endpoint = new Uri(configuration.
            GetSection("SentimentAnalysis").
            GetSection("AzureTextAIEndpoint").Value);
        _credentials = new(GetAzureAIKey());
    }

    public int GetSentimentAnalysis(string input)
    {
        var client = new TextAnalyticsClient(_endpoint, _credentials);
        var documentSentiment = client.AnalyzeSentiment(input);
        var averagePositiveSentiment = 
            GetAveragePositiveFromDocument(documentSentiment);

        return ConvertPositiveSentimentToReviewScore(averagePositiveSentiment);
    }

    private double GetAveragePositiveFromDocument(DocumentSentiment documentSentiment)
    {
        double totalPostiveSentiment = 0;
        foreach(var sentence in documentSentiment.Sentences)
        {
            totalPostiveSentiment += sentence.ConfidenceScores.Positive;
        }
        return totalPostiveSentiment / documentSentiment.Sentences.Count();
    }

    private int ConvertPositiveSentimentToReviewScore(double sentiment)
    {
        // value should be something like .86
        // now it's 8.6
        var outOfTen = sentiment * 10;
        // x = 5(outOfTen) / 10
        var reviewScore = 5 * outOfTen / 10;

        // round the double to an integer
        return Convert.ToInt32(Math.Round(reviewScore, 0));
    }

    // BAD PRACTICE I know
    // Normally I'd set this as an env variable locally or use
    // Something like Azure Key Vault
    private string GetAzureAIKey()
    {
        return "ab1ab835969f49dfbf6968cf1369760d";
    }
}