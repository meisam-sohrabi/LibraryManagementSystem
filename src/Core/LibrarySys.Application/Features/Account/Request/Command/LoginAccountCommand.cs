using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Account.Request.Command
{
    public class LoginAccountCommand : IRequest<BaseResponseDto<Tuple<string,string>>>
    {
        public LoginDto Login { get; set; }
    }
}
