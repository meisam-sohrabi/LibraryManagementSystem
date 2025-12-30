using FluentValidation;

namespace LibrarySys.Application.DTOs.Validator.Auth
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("نام نمی تواند خالی باشد")
                .NotNull().WithMessage("نام نمی تواند خالی باشد");

            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("نام کاربری نمی تواند خالی باشد")
                .NotNull().WithMessage("نام کاربری نمی تواند خالی باشد");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("ایمیل نمی تواند خالی باشد")
                .NotNull().WithMessage("ایمیل نمی تواند خالی باشد");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("شماره تماس نمی تواند خالی باشد")
                .NotNull().WithMessage("شماره تماس نمی تواند خالی باشد");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("رمز نمی تواند خالی باشد")
                .NotNull().WithMessage("رمز نمی تواند خالی باشد");
        }
    }
}
