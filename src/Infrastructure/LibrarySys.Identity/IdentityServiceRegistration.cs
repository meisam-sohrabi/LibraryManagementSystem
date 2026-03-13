using LibrarySys.Application.Contract.IdentityService;
using LibrarySys.Identity.Context;
using LibrarySys.Identity.Entity;
using LibrarySys.Identity.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace LibrarySys.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection IdentityConfiguration(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(configuration["ConnectionStrings:LibraryManagementIdentityConnection"]);
            });
            services.AddIdentity<CustomUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAlgorithms = new [] { SecurityAlgorithms.HmacSha256},
                        RequireSignedTokens = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
