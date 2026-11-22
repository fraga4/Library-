
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class EfBookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public EfBookRepository(LibraryDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public void Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void BulkLoad(IEnumerable<Book> books)
    {
        _context.Books.AddRange(books);
        _context.SaveChanges();
    }

    public Book? GetById(Guid id)
    {
        return _context.Books.FirstOrDefault(b => b.Id == id);
    }

    public IEnumerable<Book> GetAll()
    {
        return _context.Books.ToList();
    }

    public void Update(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        Book? book = GetById(id);

        if (book == null)
            throw new KeyNotFoundException("Book not found.");

        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    public IEnumerable<Book> SearchByTitle(string title)
    {
        return _context.Books
            .Where(b => EF.Functions.Like(b.Title, $"%{title}%"))
            .ToList();
    }

    public Book? GetByIsbn(string isbn)
    {
        string cleaned = isbn.Replace("-", "").Replace(" ", "");

        return _context.Books.FirstOrDefault(b => b.Isbn == cleaned);
    }

    public IEnumerable<Book> SearchByAuthor(string author)
    {
        return _context.Books
            .Where(b => EF.Functions.Like(b.Author, $"%{author}%"))
            .ToList();
    }

    public IEnumerable<Book> SearchByPublisher(string publisher)
    {
        return _context.Books
            .Where(b => EF.Functions.Like(b.Publisher, $"%{publisher}%"))
            .ToList();
    }
}
