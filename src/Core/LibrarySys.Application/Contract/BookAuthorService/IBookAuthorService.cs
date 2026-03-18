using LibrarySys.Application.DTOs;

namespace LibrarySys.Application.Contract.BookAuthorService
{
    public interface IBookAuthorService
    {
        Task<IReadOnlyList<GetBookResponseDto>> GetBookAuthor();
        Task<IEnumerable<BookAuthorFullSearchFilterResponseDto>> FullSearch(BookAuthorFullSearchRequestDto search);
        Task<BookAuthorRequestDto> GetByIdWithAuthor(Guid Id);
    }
}
