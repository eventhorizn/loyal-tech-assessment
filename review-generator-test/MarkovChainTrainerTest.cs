using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using review_api.Business;

namespace review_generator_test;

[TestClass]
public class MarkovChainTrainerTest
{
    private IMarkovChainTrainer? _markovChainTrainer;

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<IAzureAITextAnalytics, AzureAITextAnalytics>();
        services.AddSingleton<IReviewFileReader, ReviewFileReader>();
        services.AddSingleton<IRatingGenerator, RatingGenerator>();
        services.AddSingleton<IMarkovChainTrainer, MarkovChainTrainer>();
        services.AddSingleton(InitConfig());

        var serviceProvider = services.BuildServiceProvider();

        _markovChainTrainer = serviceProvider.
            GetService<IMarkovChainTrainer>();
        _markovChainTrainer?.TrainMarkovChain();
    }

    [TestMethod]
    public void MarkovChainTrainerReturnsReviewText()
    {
        var reviewText = _markovChainTrainer?.GetRandomReview();

        Assert.IsTrue(reviewText != "");
    }

    private static IConfiguration InitConfig()
    {
        return new ConfigurationBuilder().
            AddJsonFile("appsettings.test.json").Build();
    }
}