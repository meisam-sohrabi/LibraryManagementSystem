using FluentValidation;

namespace LibrarySys.Application.DTOs.Validator.Author
{
    public class AuthorRequestDtoValidator : AbstractValidator<AuthorRequestDto>
    {
        public AuthorRequestDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("نام نمی تواند خالی باشد")
                .NotNull().WithMessage("نام نمی تواند خالی باشد");

            RuleFor(c => c.BirthYear)
                .NotEmpty().WithMessage("سال تولد نمی تواند خالی باشد")
                .NotNull().WithMessage("سال تولد نمی تواند خالی باشد")
                .GreaterThan(0).WithMessage("سال تولد باید بزرگ تر از صفر باشد"); ;
        }
    }
}
