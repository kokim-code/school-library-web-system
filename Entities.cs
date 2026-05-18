namespace SchoolLibrary.Web.Models;

public enum BookCopyStatus { Available, Issued, Moved, Lost }

public sealed class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PublisherYear { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
}

public sealed class Author
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}

public sealed class BookAuthor
{
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}

public sealed class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public sealed class Shelf
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Rack { get; set; } = string.Empty;
}

public sealed class BookCopy
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public string InventoryNumber { get; set; } = string.Empty;
    public int ShelfId { get; set; }
    public Shelf Shelf { get; set; } = null!;
    public BookCopyStatus Status { get; set; } = BookCopyStatus.Available;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
}

public sealed class Reader
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}

public sealed class Loan
{
    public int Id { get; set; }
    public int BookCopyId { get; set; }
    public BookCopy BookCopy { get; set; } = null!;
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}

public sealed class Transfer
{
    public int Id { get; set; }
    public int BookCopyId { get; set; }
    public BookCopy BookCopy { get; set; } = null!;
    public int FromShelfId { get; set; }
    public int ToShelfId { get; set; }
    public DateTime TransferDate { get; set; }
    public string Comment { get; set; } = string.Empty;
}
