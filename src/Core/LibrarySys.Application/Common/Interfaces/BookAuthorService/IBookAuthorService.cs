using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Common.Interfaces.BookAuthorService
{
    public interface IBookAuthorService
    {
        Task<IReadOnlyList<GetBookResponseDto>> GetBookAuthor();
        Task<IEnumerable<BookAuthorFullSearchFilterResponseDto>> FullSearch(BookAuthorFullSearchRequestDto search);
        Task<BookAuthorRequestDto> GetByIdWithAuthor(Guid Id);
    }
}
