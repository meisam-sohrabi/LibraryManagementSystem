using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Features.Account.Request.Command
{
    public class CreateAccountCommand : IRequest<BaseResponseDto<string>>
    {
        public RegistrationRequestDto Register { get; set; }
    }
}
