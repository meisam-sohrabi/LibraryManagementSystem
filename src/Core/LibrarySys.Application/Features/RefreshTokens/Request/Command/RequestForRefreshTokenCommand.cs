using LibrarySys.Application.Common.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.RefreshTokens.Request.Command
{
    public record RequestForRefreshTokenCommand(string token) : IRequest<BaseResponseDto<Tuple<string,string>>>
    {
    }
}
