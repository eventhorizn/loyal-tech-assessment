using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using review_api.Business;

namespace review_generator_test;

[TestClass]
public class ReviewFileReaderTest
{
    private IReviewFileReader? _reviewFileReader;

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IReviewFileReader, ReviewFileReader>();
        var serviceProvider = services.BuildServiceProvider();

        _reviewFileReader = serviceProvider.GetService<IReviewFileReader>();
    }

    [TestMethod]
    public void FileReaderReturnsRows()
    {
        var items = _reviewFileReader?.ReadFile();

        Assert.IsTrue(items?.Count > 0);
    }
}