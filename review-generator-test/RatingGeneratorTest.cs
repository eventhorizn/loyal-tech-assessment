using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace review_generator_test;

[TestClass]
public class RatingGeneratorTest
{
    private IRatingGenerator? _ratingGenerator;

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRatingGenerator, RatingGenerator>();
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

    private List<int> GetRatingList(int numberRatings)
    {
        var reviews = new List<int>();

        for (var i = 0; i <= numberRatings; i++)
        {
            reviews.Add(_ratingGenerator.GenerateRandomRating());
        }

        return reviews;
    }
}