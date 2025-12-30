using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Handlers.Books.Request.Query
{
    public class GetAllBookSpQuery : IRequest<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>>
    {
    }
}
