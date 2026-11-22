using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Library.Presentation;
using Library.Services;
using Library.Services.Interfaces;
var context = new LibraryDbContext();

IBookRepository repository = new EfBookRepository(context);
IBookService bookService = new BookService(repository);

var menu = new ConsoleMenu(bookService);
menu.Run();
