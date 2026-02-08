using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure
{
    public interface IBorrowingRepository : IGenericRepositry<Borrowing>
    {
        Task<bool> isBorrowedAsync(Guid bookId);
        Task<List<Borrowing>> GetByMemberIdAsync(Guid memberId);
    }
}
