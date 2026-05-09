using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Features.Books.Request.Query
{
    public record GetBookQuery(Guid Id) : IRequest<BaseResponseDto<BookAuthorRequestDto>>
    {
    }
}
