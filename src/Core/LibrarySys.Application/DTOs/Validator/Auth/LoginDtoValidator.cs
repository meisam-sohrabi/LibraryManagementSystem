using FluentValidation;

namespace LibrarySys.Application.DTOs.Validator.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("نام کاربری نمی تواند خالی باشد")
                .NotNull().WithMessage("نام کاربری نمی تواند خالی باشد");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("پسورد نمی تواند خالی باشد")
                .NotNull().WithMessage("پسورد نمی تواند خالی باشد");
        }
    }
}
