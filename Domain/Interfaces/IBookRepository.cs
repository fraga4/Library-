using Library.Domain.Entities;

namespace Library.Domain.Interfaces;

public interface IBookRepository
{
    // Standard CRUD Operations
    void Add(Book book);
    void BulkLoad(IEnumerable<Book> books);
    Book? GetById(Guid id);
    IEnumerable<Book> GetAll();
    void Update(Book book);
    void Delete(Guid id);

    // Search Operations
    IEnumerable<Book> SearchByTitle(string title);
    Book? GetByIsbn(string isbn);
    IEnumerable<Book> SearchByAuthor(string author);
    IEnumerable<Book> SearchByPublisher(string publisher);
}
