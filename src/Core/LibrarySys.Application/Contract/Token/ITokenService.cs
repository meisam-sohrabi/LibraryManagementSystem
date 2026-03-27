using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Token
{
    public interface ITokenService
    {
        string GenerateJWT(User user);
        string GenerateRefreshToken();
        string TokenHash(string token);
    }
}
