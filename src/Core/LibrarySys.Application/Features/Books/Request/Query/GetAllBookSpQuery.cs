using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Books.Request.Query
{
    public class GetAllBookSpQuery : IRequest<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>>
    {
    }
}
