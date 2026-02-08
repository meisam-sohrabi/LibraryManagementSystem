using FluentValidation;
using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.IdentityService;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Option;
using LibrarySys.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace LibrarySys.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<JwtOption> _jwtConfiguration;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegistrationRequestDto> _registrationValidator;

        public AuthService(UserManager<CustomUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IOptions<JwtOption> jwtConfiguration,
            IValidator<LoginDto> loginValidator,
            IValidator<RegistrationRequestDto> registrationValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _jwtConfiguration = jwtConfiguration;
            _loginValidator = loginValidator;
            _registrationValidator = registrationValidator;
        }
        public async Task<BaseResponseDto<string>> Login(LoginDto login)
        {
            var output = new BaseResponseDto<string>();
            var validation = await _loginValidator.ValidateAsync(login);
            if (!validation.IsValid)
            {
                output.Message = "خطاهای اعتبارسنجی رخ داده است.";
                output.Success = false;
                output.StatusCode = HttpStatusCode.BadRequest;
                output.ValidationErrors = validation.ToDictionary();
                return output;
            }

            var userExist = await _userManager.Users.Where(c => c.UserName == login.Username).AsNoTracking().FirstOrDefaultAsync();
            if (userExist == null || !(await _userManager.CheckPasswordAsync(userExist, login.Password)))
            {
                output.Message = "نام کاربری یا رمز عبور اشتباه است";
                output.StatusCode = HttpStatusCode.BadRequest;
                output.Success = false;
                return output;
            }

            var accessToken = await GenerateAccessToken(userExist);
            output.Message = "کاربر با موفقیت ورود کرد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = string.Concat("Bearer ", accessToken);
            return output;
        }

        public async Task<BaseResponseDto<string>> Register(RegistrationRequestDto registrationRequest)
        {
            var output = new BaseResponseDto<string>();
            var validation = await _registrationValidator.ValidateAsync(registrationRequest);
            if (!validation.IsValid)
            {
                output.Message = "خطاهای اعتبارسنجی رخ داده است.";
                output.Success = false;
                output.StatusCode = HttpStatusCode.BadRequest;
                output.ValidationErrors = validation.ToDictionary();
                return output;
            }
            var userExist = await _userManager.FindByEmailAsync(registrationRequest.Email);
            if (userExist != null)
            {
                output.Message = "کاربر قبلا ایجاد شده";
                output.StatusCode = HttpStatusCode.Conflict;
                output.Success = true;
                output.Data = userExist.Id;
                return output;
            }

            var identityUser = new CustomUser
            {
                FullName = registrationRequest.FullName,
                Email = registrationRequest.Email,
                UserName = registrationRequest.UserName,
                PhoneNumber = registrationRequest.PhoneNumber
            };
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var result = await _userManager.CreateAsync(identityUser, registrationRequest.Password);
                if (result.Succeeded)
                {
                    var roleExist = await _roleManager.RoleExistsAsync("User");
                    if (!roleExist)
                    {
                        var identityRole = new IdentityRole
                        {
                            Name = "User",
                            NormalizedName = "USER"
                        };
                        await _roleManager.CreateAsync(identityRole);
                    }
                    await _userManager.AddToRoleAsync(identityUser, "User");
                    await _unitOfWork.CommitTransactionAsync();
                }
            }
            catch (Exception ex)
            {

                await _unitOfWork.RollBackTransactionAsync();

                output.Message = "خطای غیرمنتظره رخ داد" + ex.Message;
                output.Success = false;
                output.StatusCode = HttpStatusCode.InternalServerError;
                return output;
            }
            output.Message = "کاربر با موفقیت ایجاد شد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = identityUser.Id;
            return output;
        }

        public async Task<bool> UserExist(string Email)
        {
            return await _userManager.Users.AnyAsync(c => c.Email == Email);
        }

        private async Task<string> GenerateAccessToken(CustomUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var secreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Value.Key));
            var signingKey = new SigningCredentials(secreteKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id),
                new(ClaimTypes.Name,user.FullName),
                new(ClaimTypes.Email,user.Email),
            };
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var jwtConfig = new JwtSecurityToken(
            issuer: _jwtConfiguration.Value.Issuer,
            audience: _jwtConfiguration.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingKey);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtConfig);
            return token;
        }
    }
}
