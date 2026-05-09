using LibrarySys.Application.Common.Interfaces.Token;
using LibrarySys.Application.Option;
using LibrarySys.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Service
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtOption> _jwtOptoin;

        public TokenService(IOptions<JwtOption> jwtOptoin)
        {
            _jwtOptoin = jwtOptoin;
        }
        public string GenerateJWT(User user)
        {
            var secreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptoin.Value.Key));
            var signingKey = new SigningCredentials(secreteKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id),
                new(ClaimTypes.Name,user.FullName),
                new(ClaimTypes.Email,user.Email ?? string.Empty),
                new(ClaimTypes.Role,user.Role.ToString())
            };

            var jwtConfig = new JwtSecurityToken(
            issuer: _jwtOptoin.Value.Issuer,
            audience: _jwtOptoin.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingKey);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtConfig);
            return token;
        }

        public string GenerateRefreshToken()
        {
            return $"{Guid.NewGuid().ToString()}{DateTime.Now:yyyymmdd}";
        }

        public string TokenHash(string token)
        {
            return BCrypt.Net.BCrypt.HashPassword(token);
        }
    }
}
