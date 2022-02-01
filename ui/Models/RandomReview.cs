// Normally, I'd create a separate nuget model package
// Since this model is shared b/t api and ui
using System.Text.Json.Serialization;

namespace ui.Models;

public record RandomReview
{
    [JsonPropertyName("reviewText")]
    public string? ReviewText {get; set;}
    [JsonPropertyName("rating")]
    public int Rating {get; set;}
}