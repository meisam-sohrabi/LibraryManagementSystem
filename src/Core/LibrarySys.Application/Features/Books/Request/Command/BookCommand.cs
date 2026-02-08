using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Books.Request.Command
{
    public class BookCommand : IRequest<BaseResponseDto<BookAuthorRequestDto>>
    {
        public BookAuthorRequestDto BookAuthorRequest { get; set; }
    }
}
