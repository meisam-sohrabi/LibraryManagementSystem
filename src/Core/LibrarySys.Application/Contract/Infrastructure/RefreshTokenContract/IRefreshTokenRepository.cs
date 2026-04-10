using LibrarySys.Application.Contract.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure.RefreshTokenContract
{
    public interface IRefreshTokenRepository : IGenericRepositry<RefreshToken>
    {
        Task<RefreshToken> GetToken(string refreshToken);
        Task<RefreshToken> GetTokenWithFlag(string refreshToken);
        Task<RefreshToken> GetTokenByUserId(string userId);
    }
}
