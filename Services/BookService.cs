using System.Text.Json;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Services.DTOs;
using Library.Services.Interfaces;
using static System.DateTime;

namespace Library.Services;

public class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public void AddBook(Book book)
    {
        _repository.Add(book);
    }

    public void UpdateBook(Book book)
    {
        _repository.Update(book);
    }

    public void BulkLoad(string filePath)
    {
        LoadBooksFromJson(filePath);
    }

    public void LoadBooksFromJson(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("JSON file not found.", filePath);

        string jsonContent = File.ReadAllText(filePath);

        List<BookJsonDto>? rawBooks =
        JsonSerializer.Deserialize<List<BookJsonDto>>(jsonContent);
        Console.WriteLine(rawBooks?[0].Title);

        if (rawBooks == null)
            throw new InvalidDataException("Unable to read books from JSON.");

        List<Book> books = [.. rawBooks.Select(dto => new Book(
            Guid.NewGuid(),
            dto.Title,
            string.Join(", ", dto.Authors ?? []),
            dto.PublishedDate ?? DateTime.MinValue,
            string.Join(", ", dto.Genres ?? []),
            dto.Isbn,
            dto.Publisher,
            dto.TotalPages ?? 0,
            dto.Rating,
            0,
            dto.BookId
        ))];

        _repository.BulkLoad(books);
    }

    public Book? GetBookById(Guid id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _repository.GetAll();
    }

    public IEnumerable<Book> SearchBooksByTitle(string title)
    {
        return _repository.SearchByTitle(title);
    }

    public Book? GetBookByIsbn(string isbn)
    {
        return _repository.GetByIsbn(isbn);
    }

    public IEnumerable<Book> SearchBooksByAuthor(string author)
    {
        return _repository.SearchByAuthor(author);
    }

    public IEnumerable<Book> SearchBooksByPublisher(string publisher)
    {
        return _repository.SearchByPublisher(publisher);
    }

    public void DeleteBook(string isbn)
    {
        Book? book = _repository.GetByIsbn(isbn);

        if (book == null)
            throw new KeyNotFoundException("Book not found.");

        _repository.Delete(book.Id);
    }
}
