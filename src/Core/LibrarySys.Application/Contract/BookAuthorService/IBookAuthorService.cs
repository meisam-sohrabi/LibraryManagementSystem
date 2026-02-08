using LibrarySys.Application.DTOs;

namespace LibrarySys.Application.Contract.BookAuthorService
{
    public interface IBookAuthorService
    {
        Task<IReadOnlyList<GetBookResponseDto>> GetBookAuthor();
        Task<BookAuthorRequestDto> GetByIdWithAuthor(Guid Id);
    }
}
