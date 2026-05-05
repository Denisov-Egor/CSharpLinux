public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearOfPublication { get; set; }

    public Book(string title, string author, int yearOfPublication)
    {
        Title = title;
        Author = author;
        YearOfPublication = yearOfPublication;
    }

    public void ChangeTitle(string newTitle)
    {
        Title = newTitle;
    }

    public void ChangeYearOfPublication(int newYear)
    {
        YearOfPublication = newYear;
    }
}

public class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Library Books:");
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year of Publication: {book.YearOfPublication}");
        }
    }

    public int GetNumberOfBooks()
    {
        return books.Count;
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        Console.WriteLine("Enter book title:");
        string title = Console.ReadLine();

        Console.WriteLine("Enter book author:");
        string author = Console.ReadLine();

        Console.WriteLine("Enter year of publication:");
        int yearOfPublication = int.Parse(Console.ReadLine());

        Book book1 = new Book(title, author, yearOfPublication);
        library.AddBook(book1);

        Console.WriteLine("Enter new title:");
        string newTitle = Console.ReadLine();
        book1.ChangeTitle(newTitle);

        Console.WriteLine("Enter new year of publication:");
        int newYear = int.Parse(Console.ReadLine());
        book1.ChangeYearOfPublication(newYear);
        library.RemoveBook(book1); 
        library.AddBook(book1); 

        library.DisplayBooks();
    }
}