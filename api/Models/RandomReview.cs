namespace review_api.Models;

public record RandomReview
{
    public string? ReviewText {get; set;}
    public int Rating {get; set;}
}