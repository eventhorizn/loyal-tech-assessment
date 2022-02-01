using Markov;

namespace review_api.Business;

public class MarkovChainTrainer : IMarkovChainTrainer
{
    private readonly IReviewFileReader _reviewFileReader;
    private readonly MarkovChain<string> _markovChain;
    private readonly int _rowsToTrain;

    public MarkovChainTrainer(IReviewFileReader reviewFileReader, IConfiguration configuration)
    {
        _reviewFileReader = reviewFileReader;
        _markovChain = new MarkovChain<string>(
            int.Parse(configuration.
            GetSection("MarkovChainSettings").
            GetSection("Depth").Value));
        _rowsToTrain = int.Parse(
            configuration.
            GetSection("MarkovChainSettings").
            GetSection("Rows").Value);
    }

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