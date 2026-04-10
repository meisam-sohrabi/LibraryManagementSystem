using LibrarySys.Application.Contract.Infrastructure.RefreshTokenContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetToken(string refreshToken)
        {
          return  await _context.refreshToken.Include(c=> c.User).FirstOrDefaultAsync(c=> c.Token == refreshToken);   
        }

        public async Task<RefreshToken> GetTokenByUserId(string userId)
        {
            return await _context.refreshToken.FirstOrDefaultAsync(c=> c.UserId == userId);  
        }

        public async Task<RefreshToken> GetTokenWithFlag(string refreshToken)
        {
            return await _context.refreshToken.Where(c=> c.IsRevoked == false ).Where(c=> c.Token == refreshToken).Include(c => c.User).FirstOrDefaultAsync();

        }
    }
}
