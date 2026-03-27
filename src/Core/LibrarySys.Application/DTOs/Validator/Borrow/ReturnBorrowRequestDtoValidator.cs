using FluentValidation;
using LibrarySys.Application.Contract.Infrastructure.BorrowingContract;
namespace LibrarySys.Application.DTOs.Validator.Borrow
{
    public class ReturnBorrowRequestDtoValidator : AbstractValidator<ReturnBorrowRequestDto>
    {
        private readonly IBorrowingRepository _borrowRepo;

        public ReturnBorrowRequestDtoValidator(IBorrowingRepository borrowRepo)
        {
            _borrowRepo = borrowRepo;
            RuleFor(c => c.ReturnDate)
                .NotEmpty().WithMessage("تاریخ بازگشت کتاب باید ثبت شود")
                .NotNull().WithMessage("تاریخ بازگشت کتاب باید مقدار داشته باشد");

            RuleFor(c => c.ReturnCode)
                .NotNull().WithMessage("کد بازگشت باید وارد شود")
                .NotEmpty().WithMessage("کد بازگشت باید وارد شود")
                .MustAsync(CheckReturnCode)
                .WithMessage("کد وارد شده اشتباه می باشد");

        }

        private async Task<bool> CheckReturnCode(Guid Code, CancellationToken token)
        {
            return await _borrowRepo.ReturnCodeExist(Code);
        }

    }
}
