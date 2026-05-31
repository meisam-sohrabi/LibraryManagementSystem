using LibrarySys.Application.Common.DTOs.Validator.Books;
using LibrarySys.Application.Features.Books.Request.Command;

namespace LibrarySys.Application.Features.Books.Handler.Command
{
    public class BookCommandValidator:AbstractValidator<BookCommand>
    {
        public BookCommandValidator()
        {
            RuleFor(c=> c.BookAuthorRequest).SetValidator(new BookAuthRequestDtoValidator());
        }
    }
}
