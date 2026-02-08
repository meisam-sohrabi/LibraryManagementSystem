using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Books.Request.Query
{
    public record GetBookQuery(Guid Id) : IRequest<BaseResponseDto<BookAuthorRequestDto>>
    {
    }
}
