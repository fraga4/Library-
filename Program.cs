using Library.Domain.Interfaces;
using Library.Infrastructure.Repositories;
using Library.Presentation;
using Library.Services;
using Library.Services.Interfaces;

IBookRepository repository = new InMemoryBookRepository();
IBookService bookService = new BookService(repository);

var menu = new ConsoleMenu(bookService);
menu.Run();
