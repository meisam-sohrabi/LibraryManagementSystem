using FluentValidation;
using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Common.DTOs.Validator.Auth
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
