using Library.Domain.Entities;
using Library.Services.Interfaces;

namespace Library.Presentation;

public class ConsoleMenu
{
    private readonly IBookService _bookService;
    private bool _isRunning = true;

    public ConsoleMenu(IBookService bookService)
    {
        _bookService = bookService;
    }

    public void Run()
    {
        _isRunning = true;

        while (_isRunning)
        {
            DisplayMenu();
            string? option = Console.ReadLine();
            Console.WriteLine();

            try
            {
                ProcessOption(option);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (_isRunning)
            {
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("       LIBRARY MANAGEMENT SYSTEM        ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Register New Book");
        Console.WriteLine("2. List All Books");
        Console.WriteLine("3. Search Book by Title");
        Console.WriteLine("4. Search Book by ISBN");
        Console.WriteLine("5. Search Book by Author");
        Console.WriteLine("6. Search Book by Publisher");
        Console.WriteLine("7. Delete Book");
        Console.WriteLine("8. Bulk Load Books from JSON");
        Console.WriteLine("9. Update Book");
        Console.WriteLine("0. Exit");
        Console.WriteLine("========================================");
        Console.Write("Select an option: ");
    }

    private void ProcessOption(string? option)
    {
        switch (option)
        {
            case "1": ExecuteAddBook(); break;
            case "2": ExecuteListBooks(); break;
            case "3": ExecuteSearchByTitle(); break;
            case "4": ExecuteSearchByIsbn(); break;
            case "5": ExecuteSearchByAuthor(); break;
            case "6": ExecuteSearchByPublisher(); break;
            case "7": ExecuteDeleteBook(); break;
            case "8": ExecuteBulkLoad(); break;
            case "9": ExecuteUpdateBook(); break;
            case "0":
                Console.WriteLine("Exiting Library Management System. Goodbye!");
                _isRunning = false;
                break;
            default: Console.WriteLine("Invalid option!"); break;
        }
    }

    private void ExecuteAddBook()
{
    Console.WriteLine("--- Register New Book ---");

    string title = ReadText("Title: ");
    string author = ReadText("Author: ");
    string date = ReadText("Published Date (yyyy-MM-dd): ");
    string genre = ReadText("Genre: ");
    string isbn = ReadText("ISBN: ");
    string publisher = ReadText("Publisher: ");
    int pages = ReadInt("Pages: ");
    decimal rating = decimal.Parse(ReadText("Rating: "));
    int stock = ReadInt("Stock: ");

    Book book = new Book(
        Guid.NewGuid(),
        title,
        author,
        DateTime.Parse(date),
        genre,
        isbn,
        publisher,
        pages,
        rating,
        stock
    );

    _bookService.AddBook(book);

    Console.WriteLine("Book added successfully.");
}

    private void ExecuteListBooks()
    {
        Console.WriteLine("--- Book Collection ---");
        IEnumerable<Book> books = _bookService.GetAllBooks();
        PrintBooks(books);
    }

    private void ExecuteSearchByTitle()
    {
        string title = ReadText("Title: ");
        PrintBooks(_bookService.SearchBooksByTitle(title));
    }

    private void ExecuteSearchByIsbn()
    {
        string isbn = ReadText("ISBN: ");
        Book? book = _bookService.GetBookByIsbn(isbn);

        if (book == null)
            Console.WriteLine("Book not found.");
        else
            PrintBook(book);
    }

    private void ExecuteSearchByAuthor()
    {
        string author = ReadText("Author: ");
        PrintBooks(_bookService.SearchBooksByAuthor(author));
    }

    private void ExecuteSearchByPublisher()
    {
        string publisher = ReadText("Publisher: ");
        PrintBooks(_bookService.SearchBooksByPublisher(publisher));
    }

    private void ExecuteDeleteBook()
    {
        string isbn = ReadText("ISBN: ");
        _bookService.DeleteBook(isbn);
        Console.WriteLine("Book deleted successfully.");
    }

    private void ExecuteBulkLoad()
    {
        _bookService.LoadBooksFromJson("basededados.json");
        Console.WriteLine("Books loaded successfully.");
    }

    private void ExecuteUpdateBook()
{
    string isbn = ReadText("Current ISBN: ");
    Book? book = _bookService.GetBookByIsbn(isbn);

    if (book == null)
    {
        Console.WriteLine("Book not found.");
        return;
    }

    string title = ReadText("New title: ");
    string author = ReadText("New author: ");
    DateTime publishedDate = DateTime.Parse(ReadText("New published date (yyyy-MM-dd): "));
    string genre = ReadText("New genre: ");
    string newIsbn = ReadText("New ISBN: ");
    string publisher = ReadText("New publisher: ");
    int pages = ReadInt("New pages: ");
    decimal rating = decimal.Parse(ReadText("New rating: "));

    book.UpdateDetails(title, author, publishedDate, genre, newIsbn, publisher, pages, rating);

    _bookService.UpdateBook(book);

    Console.WriteLine("Book updated successfully.");
}

    private static string ReadText(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? "";
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static void PrintBooks(IEnumerable<Book> books)
    {
        List<Book> list = books.ToList();

        if (list.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach (Book book in list)
        {
            PrintBook(book);
            Console.WriteLine("----------------------------------------");
        }
    }

    private static void PrintBook(Book book)
    {
        Console.WriteLine($"Id: {book.Id}");
        Console.WriteLine($"Title: {book.Title}");
        Console.WriteLine($"Author: {book.Author}");
        Console.WriteLine($"Year: {book.Year}");
        Console.WriteLine($"Genre: {book.Genre}");
        Console.WriteLine($"ISBN: {book.Isbn}");
        Console.WriteLine($"Publisher: {book.Publisher}");
        Console.WriteLine($"Pages: {book.Pages}");
        Console.WriteLine($"Stock: {book.Stock}");
    }
}
