using Markov;

public class MarkovChainTrainer : IMarkovChainTrainer
{
    private readonly IReviewFileReader _reviewFileReader;
    private readonly IConfiguration _configuration;
    private readonly MarkovChain<string> _markovChain;

    public MarkovChainTrainer(IReviewFileReader reviewFileReader, IConfiguration configuration)
    {
        _reviewFileReader = reviewFileReader;
        _configuration = configuration;
        _markovChain = new MarkovChain<string>(
            int.Parse(configuration.
            GetSection("MarkovChainSettings").
            GetSection("Depth").Value));
    }

    public void TrainMarkovChain()
    {
        var reviews = _reviewFileReader.ReadFile();

        foreach(var review in reviews)
        {
            _markovChain.Add(review.ReviewText.Split(" "));
        }
    }

    private IList<string[]> GetStringArraysFromReviews(IList<ReviewData> reviews)
    {
        return (from review in reviews.Select(x => x.ReviewText)
                let strArr = review.Split(" ")
                select strArr).ToList();
    }

    public string GetRandomReview()
    {
        var rand = new Random();
        var review = "";

        for (var i = 0; i < 100; i++)
        {
            review = string.Join(" ", _markovChain.Chain(rand));
        }

        return review;
    }
}