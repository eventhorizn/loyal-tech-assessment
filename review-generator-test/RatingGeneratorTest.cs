using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using review_api.Business;

namespace review_generator_test;

[TestClass]
public class RatingGeneratorTest
{
    private IRatingGenerator? _ratingGenerator;

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IAzureAITextAnalytics, AzureAITextAnalytics>();
        services.AddSingleton<IRatingGenerator, RatingGenerator>();
        services.AddSingleton(InitConfig());
        var serviceProvider = services.BuildServiceProvider();

        _ratingGenerator = serviceProvider.GetService<IRatingGenerator>();
    }

    [TestMethod]
    public void RandomRatingIsRandom()
    {
        var reviews = GetRatingList(10);
        var allUnique = reviews.Distinct().Count() == 1;

        Assert.IsFalse(allUnique);
    }

    [TestMethod]
    public void SentimentRatingGeneratesRating()
    {
        var inputText = "I had the best day of my life. I wish you were there with me.";
        var rating = _ratingGenerator?.GenerateSentimentRating(inputText);

        Assert.IsTrue(rating > 0);
    }

    private List<int> GetRatingList(int numberRatings)
    {
        var reviews = new List<int>();

        if (_ratingGenerator is null) return reviews;

        for (var i = 0; i <= numberRatings; i++)
        {
            reviews.Add(_ratingGenerator.GenerateRandomRating());
        }

        return reviews;
    }

    private static IConfiguration InitConfig()
    {
        return new ConfigurationBuilder().
            AddJsonFile("appsettings.test.json").Build();
    }
}