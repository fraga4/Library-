
using System.Text.Json.Serialization;

namespace Library.Services.DTOs;

public class BookJsonDto
{
    [JsonPropertyName("book_id")]
    public int BookId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("total_pages")]
    public int? TotalPages { get; set; }

    [JsonPropertyName("rating")]
    public decimal Rating { get; set; }

    [JsonPropertyName("isbn")]
    public string? Isbn { get; set; }

    [JsonPropertyName("published_date")]
    public DateTime? PublishedDate { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = "";

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; } = new();

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; } = new();
}
