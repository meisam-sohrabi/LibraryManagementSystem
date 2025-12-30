using LibrarySys.Application.Contract.Identity;
using LibrarySys.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<BaseResponseDto<string>> Register([FromBody]RegistrationRequestDto registrationRequest)
        {
           return await _authService.Register(registrationRequest);   
        }

        [HttpPost("Login")]
        public async Task<BaseResponseDto<string>> Login([FromBody]LoginDto loginRequest)
        {
            return await _authService.Login(loginRequest);  
        }
    }
}
