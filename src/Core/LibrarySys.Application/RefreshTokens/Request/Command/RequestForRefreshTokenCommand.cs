using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.RefreshTokens.Request.Command
{
    public record RequestForRefreshTokenCommand(string token) : IRequest<BaseResponseDto<Tuple<string,string>>>
    {
    }
}
