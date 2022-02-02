namespace review_api.Business;

public interface IAzureAITextAnalytics
{
    int GetSentimentAnalysis(string input);
}