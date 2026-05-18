using Microsoft.EntityFrameworkCore;
using SchoolLibrary.Web.Data;
using SchoolLibrary.Web.Models;

namespace SchoolLibrary.Web.Services;

public interface ILoanService
{
    Task IssueBookAsync(int copyId, int readerId, DateTime dueDate);
    Task ReturnBookAsync(int loanId);
}

public sealed class LoanService : ILoanService
{
    private readonly LibraryDbContext db;
    public LoanService(LibraryDbContext db) => this.db = db;

    public async Task IssueBookAsync(int copyId, int readerId, DateTime dueDate)
    {
        var copy = await db.BookCopies.FirstOrDefaultAsync(x => x.Id == copyId);
        if (copy is null) throw new InvalidOperationException("Экземпляр книги не найден.");
        if (copy.Status != BookCopyStatus.Available) throw new InvalidOperationException("Экземпляр недоступен для выдачи.");

        copy.Status = BookCopyStatus.Issued;
        db.Loans.Add(new Loan
        {
            BookCopyId = copyId,
            ReaderId = readerId,
            IssueDate = DateTime.UtcNow,
            DueDate = dueDate
        });
        await db.SaveChangesAsync();
    }

    public async Task ReturnBookAsync(int loanId)
    {
        var loan = await db.Loans.Include(x => x.BookCopy).FirstOrDefaultAsync(x => x.Id == loanId);
        if (loan is null) throw new InvalidOperationException("Запись выдачи не найдена.");
        if (loan.ReturnDate is not null) throw new InvalidOperationException("Книга уже возвращена.");
        loan.ReturnDate = DateTime.UtcNow;
        loan.BookCopy.Status = BookCopyStatus.Available;
        await db.SaveChangesAsync();
    }
}
