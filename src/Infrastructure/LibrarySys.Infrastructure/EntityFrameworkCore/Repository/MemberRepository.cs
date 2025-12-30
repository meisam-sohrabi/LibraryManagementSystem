using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext context) : base(context)
        {
        }
    }
}
