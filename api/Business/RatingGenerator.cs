using System.Security.Cryptography;

namespace review_api.Business;

public class RatingGenerator : IRatingGenerator
{
    public int GenerateRandomRating()
    {
        return RandomNumberGenerator.GetInt32(1, 6);
    }
}