using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using review_api.Business;

namespace review_generator_test;

[TestClass]
public class ReviewGeneratorTest
{
    private IReviewGenerator? _reviewGenerator;

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<IAzureAITextAnalytics, AzureAITextAnalytics>();
        services.AddSingleton<IReviewFileReader, ReviewFileReader>();
        services.AddSingleton<IRatingGenerator, RatingGenerator>();
        services.AddSingleton<IMarkovChainTrainer, MarkovChainTrainer>();
        services.AddSingleton<IReviewGenerator, ReviewGenerator>();
        services.AddSingleton(InitConfig());

        var serviceProvider = services.BuildServiceProvider();

        var markovChainTrainer = serviceProvider.
            GetService<IMarkovChainTrainer>();
        if (markovChainTrainer is not null)
            markovChainTrainer.TrainMarkovChain();

        _reviewGenerator = serviceProvider.GetService<IReviewGenerator>();
    }

    [TestMethod]
    public void ReviewGeneratorReturnsReview()
    {
        var review = _reviewGenerator?.GenerateRandomReview();

        Assert.IsTrue(review?.Rating > 0);
        Assert.IsTrue(review?.ReviewText != "");
    }

    private static IConfiguration InitConfig()
    {
        return new ConfigurationBuilder().
            AddJsonFile("appsettings.test.json").Build();
    }
}