using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Common.Interfaces.IdentityService
{
    public interface IAuthService
    {
        Task<BaseResponseDto<string>> Register(RegistrationRequestDto registrationRequest);
        Task<BaseResponseDto<OTPResponseDto>> OneTimePasswordGenerator(string phoneNumber);
        Task<BaseResponseDto<string>> Login(LoginDto login);
        Task<bool> UserExist(string Email);
    }
}
