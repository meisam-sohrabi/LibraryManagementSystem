using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces.IdentityService;
using LibrarySys.Application.Features.Account.Request.Command;
using LibrarySys.Application.Features.RefreshTokens.Request.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public AccountController(IAuthService authService,IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<BaseResponseDto<string>> Create([FromBody] RegistrationRequestDto registrationRequest)
        {
            CreateAccountCommand command = new CreateAccountCommand { Register = registrationRequest };
            return await _mediator.Send(command);  
        }


        [HttpPost("Login")]
        public async Task<BaseResponseDto<Tuple<string, string>>> Login([FromBody] LoginDto loginRequest)
        {
            // if we use behavior we need to add try catch for controllers 

            //try
            //{
                LoginAccountCommand command = new LoginAccountCommand { Login = loginRequest };
                return await _mediator.Send(command);
            //}
            //catch (Exception ex)
            //{
            //   return new BaseResponseDto<Tuple<string, string>> { };
            //}
        }


        [HttpPost("Register")]
        public async Task<BaseResponseDto<string>> Register([FromBody]RegistrationRequestDto registrationRequest)
        {
           return await _authService.Register(registrationRequest);   
        }

        [HttpPost("OTP")]
        public async Task<BaseResponseDto<OTPResponseDto>> OneTimePasswordGenerator([FromBody]string phoneNumber)
        {
            return await _authService.OneTimePasswordGenerator(phoneNumber);
        }


        [HttpPost("RefreshToken")]
        public async Task<BaseResponseDto<Tuple<string,string>>> RefreshToken([FromBody] string tokne)
        {
            RequestForRefreshTokenCommand command = new RequestForRefreshTokenCommand(tokne);
            return await _mediator.Send(command);
        }


        //[HttpPost("Login")]
        //public async Task<BaseResponseDto<string>> Login([FromBody]LoginDto loginRequest)
        //{
        //    return await _authService.Login(loginRequest);  
        //}
    }
}
