
using LibrarySys.Application.Common.DTOs.Validator.Author;

namespace LibrarySys.Application.Common.DTOs.Validator.Books
{
    public class BookAuthRequestDtoValidator : AbstractValidator<BookAuthorRequestDto>
    {
        public BookAuthRequestDtoValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("عنوان نمی تواند خالی باشد")
                .NotNull().WithMessage("عنوان نمی تواند خالی باشد");

            RuleFor(c=> c.Genere)
                .NotEmpty().WithMessage("ژانر نمی تواند خالی باشد")
                .NotNull().WithMessage("ژانر نمی تواند خالی باشد");

            RuleFor(c => c.PublishYear)
                .NotEmpty().WithMessage("سال انتشار نمی تواند خالی باشد")
                .NotNull().WithMessage("سال انتشار نمی تواند خالی باشد")
                .GreaterThan(0).WithMessage("نسخه های کتاب باید بزرگ تر از صفر باشد"); ;

            RuleFor(c => c.AvailableCopies)
                .NotEmpty().WithMessage("نسخه های کتاب نمی تواند خالی باشد")
                .NotNull().WithMessage("نسخه های کتاب نمی تواند خالی باشد")
                .GreaterThan(0).WithMessage("نسخه های کتاب باید بزرگ تر از صفر باشد");

            RuleForEach(c => c.Authors)
                .SetValidator(new AuthorRequestDtoValidator());
                
        }
    }
}
