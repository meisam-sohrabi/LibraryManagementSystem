using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class BorrowingRepository : GenericRepository<Borrowing>, IBorrowingRepository
    {
        public BorrowingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
