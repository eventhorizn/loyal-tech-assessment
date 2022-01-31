using System.Security.Cryptography;

public class RatingGenerator : IRatingGenerator
{
    public int GenerateRandomRating()
    {
        return RandomNumberGenerator.GetInt32(1, 5);
    }
}