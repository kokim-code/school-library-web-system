using Microsoft.EntityFrameworkCore;
using SchoolLibrary.Web.Data;
using SchoolLibrary.Web.Models;

namespace SchoolLibrary.Web.Services;

public interface ITransferService
{
    Task TransferCopyAsync(int copyId, int toShelfId, string comment);
}

public sealed class TransferService : ITransferService
{
    private readonly LibraryDbContext db;
    public TransferService(LibraryDbContext db) => this.db = db;

    public async Task TransferCopyAsync(int copyId, int toShelfId, string comment)
    {
        var copy = await db.BookCopies.FirstOrDefaultAsync(x => x.Id == copyId);
        if (copy is null) throw new InvalidOperationException("Экземпляр не найден.");
        if (copy.ShelfId == toShelfId) throw new InvalidOperationException("Нельзя переместить книгу на ту же полку.");

        var fromShelfId = copy.ShelfId;
        copy.ShelfId = toShelfId;
        copy.Status = BookCopyStatus.Moved;
        db.Transfers.Add(new Transfer
        {
            BookCopyId = copyId,
            FromShelfId = fromShelfId,
            ToShelfId = toShelfId,
            TransferDate = DateTime.UtcNow,
            Comment = comment
        });
        await db.SaveChangesAsync();
    }
}
