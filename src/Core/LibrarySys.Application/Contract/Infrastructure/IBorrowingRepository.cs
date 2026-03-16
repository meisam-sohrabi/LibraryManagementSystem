using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure
{
    public interface IBorrowingRepository : IGenericRepositry<Borrowing>
    {
        Task<bool> isBorrowedAsync(Guid bookId);
        Task<List<Borrowing>> GetAllByMemberIdAsync(Guid memberId);
        Task<Borrowing> GetByReturnCodeAsync(Guid returnCode);
        Task<bool> ReturnCodeExist(Guid returnCode);
    }
}
