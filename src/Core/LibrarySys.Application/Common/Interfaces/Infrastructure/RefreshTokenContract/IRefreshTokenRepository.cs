using LibrarySys.Application.Common.Interfaces.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Common.Interfaces.Infrastructure.RefreshTokenContract
{
    public interface IRefreshTokenRepository : IGenericRepositry<RefreshToken>
    {
        Task<RefreshToken> GetToken(string refreshToken);
        Task<RefreshToken> GetTokenWithFlag(string refreshToken);
        Task<RefreshToken> GetTokenByUserId(string userId);
    }
}
