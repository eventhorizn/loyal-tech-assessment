using review_api.Models;

namespace review_api.Business;

public interface IReviewFileReader
{
    IList<ReviewData> ReadFile();
}