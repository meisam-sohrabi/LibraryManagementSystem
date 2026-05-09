using LibrarySys.Application.Common.Interfaces.Infrastructure.BorrowingContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class BorrowingRepository : GenericRepository<Borrowing>, IBorrowingRepository
    {
        private readonly AppDbContext _context;

        public BorrowingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Borrowing>> GetAllByMemberIdAsync(Guid memberId)
        {
            return await _context.Borrowing.Include(c=> c.Book)
                .Where(c => c.MemberId == memberId).ToListAsync();
        }

        public async Task<Borrowing> GetByReturnCodeAsync(Guid returnCode)
        {
            return await _context.Borrowing.FirstOrDefaultAsync(c => c.ReturnCode == returnCode);
        }

        public async Task<bool> isBorrowedAsync(Guid bookId)
        {
            return await _context.Borrowing.AnyAsync(c=> c.BookId == bookId && c.ReturnDate == null);
        }

        public async Task<bool> ReturnCodeExist(Guid returnCode)
        {
           return await _context.Borrowing.AnyAsync(c=> c.ReturnCode == returnCode);
        }
    }
}
