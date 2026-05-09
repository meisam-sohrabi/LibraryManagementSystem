using LibrarySys.Application.Common.DTOs;
namespace LibrarySys.Application.Features.Books.Request.Query
{
    public record GetBookFullSearchSpQuery : IRequest<BaseResponseDto<IEnumerable<BookAuthorFullSearchFilterResponseDto>>>
    {
        public BookAuthorFullSearchRequestDto fullSearch { get; set; }
    }
}
