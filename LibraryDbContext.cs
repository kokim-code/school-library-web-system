using Microsoft.EntityFrameworkCore;
using SchoolLibrary.Web.Models;

namespace SchoolLibrary.Web.Data;

public sealed class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<BookCopy> BookCopies => Set<BookCopy>();
    public DbSet<Reader> Readers => Set<Reader>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Shelf> Shelves => Set<Shelf>();
    public DbSet<Transfer> Transfers => Set<Transfer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId });
        modelBuilder.Entity<Book>().HasIndex(x => x.Isbn);
        modelBuilder.Entity<BookCopy>().HasIndex(x => x.InventoryNumber).IsUnique();
        modelBuilder.Entity<Reader>().HasIndex(x => new { x.FullName, x.ClassName });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(x => x.Book).WithMany(x => x.BookAuthors).HasForeignKey(x => x.BookId);
        modelBuilder.Entity<BookAuthor>()
            .HasOne(x => x.Author).WithMany(x => x.BookAuthors).HasForeignKey(x => x.AuthorId);

        modelBuilder.Entity<BookCopy>()
            .HasOne(x => x.Book).WithMany(x => x.Copies).HasForeignKey(x => x.BookId);
        modelBuilder.Entity<BookCopy>()
            .HasOne(x => x.Shelf).WithMany().HasForeignKey(x => x.ShelfId);
    }
}
