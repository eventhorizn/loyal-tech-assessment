using Markov;

namespace review_api.Business;

public class MarkovChainTrainer(IReviewFileReader reviewFileReader, IConfiguration configuration) : IMarkovChainTrainer
{
    private readonly IReviewFileReader _reviewFileReader = reviewFileReader;
    private readonly MarkovChain<string> _markovChain = new(
            int.Parse(configuration.
            GetSection("MarkovChainSettings").
            GetSection("Depth").Value));
    private readonly int _rowsToTrain = int.Parse(
            configuration.
            GetSection("MarkovChainSettings").
            GetSection("Rows").Value);

    public void TrainMarkovChain()
    {
        var reviews = _reviewFileReader.ReadFile();

        for (var i = 0; i < _rowsToTrain; i++)
        {
            _markovChain.Add(reviews[i].ReviewText.Split(" "));
        }
    }

    public string GetRandomReview()
    {
        var review = string.Join(" ", _markovChain.Chain(new Random()));

        return review;
    }
}