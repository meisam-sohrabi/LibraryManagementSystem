using FluentValidation;
using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Application.Common.Interfaces.IdentityService;
using LibrarySys.Application.Option;
using LibrarySys.Identity.Entity;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
        private readonly HttpClient _client;

        public AuthService(UserManager<CustomUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IOptions<JwtOption> jwtConfiguration,
            IValidator<LoginDto> loginValidator,
            IValidator<RegistrationRequestDto> registrationValidator,
            HttpClient client)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _jwtConfiguration = jwtConfiguration;
            _loginValidator = loginValidator;
            _registrationValidator = registrationValidator;
            _client = client;
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


         // this is sms for test sandbox works well.
        public async Task<BaseResponseDto<OTPResponseDto>> OneTimePasswordGenerator(string phoneNumber)
        {
            BaseResponseDto<OTPResponseDto> outPut = new BaseResponseDto<OTPResponseDto>();
            OTPRequestDto sendSMS = new OTPRequestDto
            {
                mobile = phoneNumber,
                templateId = 123456,
                parameters = new OTPParameter[]
                {
                    new OTPParameter
                    {
                        name = "CODE",
                        value = "12345"

                    }
                }
            };

            string apiKey = "RFTjnPsWSklNjGxinGA5A0fxC72sqU1hZwHbG6CqDHN1K4Ds";
            
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.sms.ir/v1/send/verify");
                _client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                string content = JsonSerializer.Serialize(sendSMS);
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.SendAsync(request);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        string data = await response.Content.ReadAsStringAsync();
                        OTPResponseDto deserialize = JsonSerializer.Deserialize<OTPResponseDto>(data);
                        outPut.Message = "عملیات موفق آمیز بود";
                        outPut.StatusCode = HttpStatusCode.OK;
                        outPut.Success = true;
                        outPut.Data = deserialize;
                        break;
                    case HttpStatusCode.BadRequest:
                        outPut.Message = "وقوع خطای منطقی";
                        outPut.StatusCode = HttpStatusCode.BadRequest;
                        outPut.Success = false;
                        break;
                    case HttpStatusCode.Unauthorized:
                        outPut.Message = "وجود خطا در فرآیند احراز هویت";
                        outPut.StatusCode = HttpStatusCode.Unauthorized;
                        outPut.Success = false;
                        break;
                    case HttpStatusCode.TooManyRequests:
                        outPut.Message = "تعداد درخواست غیر مجاز";
                        outPut.StatusCode = HttpStatusCode.TooManyRequests;
                        outPut.Success = false;
                        break;
                    case HttpStatusCode.InternalServerError:
                        outPut.Message = "خطای غیر منتظره";
                        outPut.StatusCode = HttpStatusCode.InternalServerError;
                        outPut.Success = false;
                        break;
                    default:
                        break; 
                }
                return outPut;

            }
            catch(HttpRequestException http)
            {
                outPut.Message = $"خطای سرور: {http.Message}";
                outPut.Success = false;
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                return outPut;
            }
            catch(TimeoutException timeout)
            {
                outPut.Message = $"ارسال درخواست با خطا مواجه شد {timeout.Message}لطفا دقایقی دیگر امتحان کنید";
                outPut.StatusCode = HttpStatusCode.RequestTimeout;
                outPut.Success = false;
                return outPut;
            }
            catch (Exception e)
            {
                outPut.Message = $"خطای سیستمی رخ داده است{e.Message}";
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Success = false;
                return outPut;
            }


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
