using System.Text.Json.Serialization;

public class ReviewData
{
    [JsonPropertyName("image")]
    public string Image {get; set;}
    [JsonPropertyName("overall")]
    public double Overall {get; set;}
    [JsonPropertyName("vote")]
    public string Vote {get; set;}
    [JsonPropertyName("verified")]
    public bool Verified {get; set;}
    [JsonPropertyName("reviewTime")]
    public string ReviewTime {get; set;}
    [JsonPropertyName("reviewerID")]
    public string ReviewerId {get; set;}
    [JsonPropertyName("asin")]
    public string Asin {get; set;}
    [JsonPropertyName("reviewerName")]
    public string ReviewerName {get; set;}
    [JsonPropertyName("reviewText")]
    public string ReviewText {get; set;}
    [JsonPropertyName("summary")]
    public string Summary {get; set;}
}