using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public void Add(Book book)
{
    _books.Add(book);
}

    public void BulkLoad(IEnumerable<Book> books)
    {
        foreach (Book book in books)
        {
            Add(book);
        }
    }

    public Book? GetById(Guid id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public IEnumerable<Book> GetAll()
    {
        return [.. _books];
    }

    public void Update(Book book)
    {
        int index = _books.FindIndex(b => b.Id == book.Id);

        if (index == -1)
            throw new KeyNotFoundException("Book not found.");

        _books[index] = book;
    }

    public void Delete(Guid id)
    {
        Book? book = GetById(id);

        if (book == null)
            throw new KeyNotFoundException("Book not found.");

        _books.Remove(book);
    }

    public IEnumerable<Book> SearchByTitle(string title)
    {
        return _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Book? GetByIsbn(string isbn)
{
    string cleaned = isbn.Replace("-", "").Replace(" ", "");

    return _books.FirstOrDefault(b =>
        string.Equals(b.Isbn, cleaned, StringComparison.OrdinalIgnoreCase));
}

    public IEnumerable<Book> SearchByAuthor(string author)
    {
        return _books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<Book> SearchByPublisher(string publisher)
    {
        return _books.Where(b => b.Publisher.Contains(publisher, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
