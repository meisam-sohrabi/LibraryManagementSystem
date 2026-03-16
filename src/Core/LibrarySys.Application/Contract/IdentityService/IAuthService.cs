using LibrarySys.Application.DTOs;

namespace LibrarySys.Application.Contract.IdentityService
{
    public interface IAuthService
    {
        Task<BaseResponseDto<string>> Register(RegistrationRequestDto registrationRequest);
        Task<BaseResponseDto<OTPResponseDto>> OneTimePasswordGenerator(string phoneNumber);
        Task<BaseResponseDto<string>> Login(LoginDto login);
        Task<bool> UserExist(string Email);
    }
}
