using Azure;
using Azure.AI.TextAnalytics;

namespace review_api.Business;

public class AzureAITextAnalytics(IConfiguration configuration) : IAzureAITextAnalytics
{
    private readonly AzureKeyCredential _credentials = new(GetAzureAIKey());
    private readonly Uri _endpoint = new(configuration.
            GetSection("SentimentAnalysis").
            GetSection("AzureTextAIEndpoint").Value);

    public int GetSentimentAnalysis(string input)
    {
        var client = new TextAnalyticsClient(_endpoint, _credentials);
        var documentSentiment = client.AnalyzeSentiment(input);
        var averagePositiveSentiment =
            GetAveragePositiveFromDocument(documentSentiment);

        return ConvertPositiveSentimentToReviewScore(averagePositiveSentiment);
    }

    private static double GetAveragePositiveFromDocument(DocumentSentiment documentSentiment)
    {
        double totalPostiveSentiment = 0;
        foreach(var sentence in documentSentiment.Sentences)
        {
            totalPostiveSentiment += sentence.ConfidenceScores.Positive;
        }
        return totalPostiveSentiment / documentSentiment.Sentences.Count();
    }

    private static int ConvertPositiveSentimentToReviewScore(double sentiment)
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
    private static string GetAzureAIKey()
    {
        return "ab1ab835969f49dfbf6968cf1369760d";
    }
}