using Library.Domain.Entities;

namespace Library.Services.Interfaces;

public interface IBookService
{
    void AddBook(Book book);
    void UpdateBook(Book book);
    void LoadBooksFromJson(string filePath);
    Book? GetBookById(Guid id);
    IEnumerable<Book> GetAllBooks();
    IEnumerable<Book> SearchBooksByTitle(string title);
    Book? GetBookByIsbn(string isbn);
    IEnumerable<Book> SearchBooksByAuthor(string author);
    IEnumerable<Book> SearchBooksByPublisher(string publisher);
    void DeleteBook(string isbn);
    void BulkLoad(string filePath);
}
