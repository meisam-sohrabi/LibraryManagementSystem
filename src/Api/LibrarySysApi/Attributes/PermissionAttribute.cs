using LibrarySys.Application.Option;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LibrarySysApi.Attributes
{

    /// <summary>
    /// this attribute check the jwt token one more time if the invader tries to overlook the algoritm
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.FirstOrDefault();
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            token = token.Substring("Bearer ".Length).Trim();

            //token.Replace("Bearer ","")

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IOptions<JwtOption>>().Value;

            var key = Encoding.UTF8.GetBytes(configuration.Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.Issuer,
                ValidAudience = configuration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwt || jwt.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            catch 
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }

    }
}
