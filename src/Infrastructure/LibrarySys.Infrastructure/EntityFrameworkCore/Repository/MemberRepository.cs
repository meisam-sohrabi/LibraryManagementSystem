using LibrarySys.Application.Contract.Infrastructure.MemberContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Member> GetMember(string Email)
        {
            return await _context.Member.Where(c => c.Email == Email).FirstOrDefaultAsync();

        }
    }
}
