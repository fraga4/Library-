using System.Text.Json.Serialization;

namespace Library.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public int? ExternalId { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public DateTime PublishedDate { get; private set; }
    public int Year => PublishedDate.Year;
    public string Genre { get; private set; }
    public string? Isbn { get; private set; }
    public string Publisher { get; private set; }
    public int Pages { get; private set; }
    public decimal Rating { get; private set; }
    public int Stock { get; private set; }

    public Book(
        Guid id,
        string title,
        string author,
        DateTime publishedDate,
        string genre,
        string? isbn,
        string publisher,
        int pages,
        decimal rating,
        int stock = 0,
        int? externalId = null)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        ExternalId = externalId;
        Title = title;
        Author = author;
        PublishedDate = publishedDate;
        Genre = genre;
        Isbn = CleanIsbn(isbn);
        Publisher = publisher;
        Pages = pages;
        Rating = rating;
        Stock = stock;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Stock += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (quantity > Stock)
            throw new InvalidOperationException("There is not enough stock.");

        Stock -= quantity;
    }

    public void UpdateDetails(
        string title,
        string author,
        DateTime publishedDate,
        string genre,
        string? isbn,
        string publisher,
        int pages,
        decimal rating)
    {
        Title = title;
        Author = author;
        PublishedDate = publishedDate;
        Genre = genre;
        Isbn = CleanIsbn(isbn);
        Publisher = publisher;
        Pages = pages;
        Rating = rating;    
    }

    private static string? CleanIsbn(string? isbn)
{
    if (string.IsNullOrWhiteSpace(isbn))
        return null;

    return isbn.Replace("-", "").Replace(" ", "");
}

}
