using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Borrowings.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using System.Net;

namespace LibrarySys.Application.Features.Borrowings.Handler.Command
{
    public class ReturnBorrowCommandHandler : IRequestHandler<ReturnBorrowCommand, BaseResponseDto<string>>
    {
        private readonly IBorrowingRepository _borrowingRepo;
        private readonly IUnitOfWork _uow;

        public ReturnBorrowCommandHandler(IBorrowingRepository borrowingRepo, IUnitOfWork uow)
        {
            _borrowingRepo = borrowingRepo;
            _uow = uow;
        }
        public async Task<BaseResponseDto<string>> Handle(ReturnBorrowCommand request, CancellationToken cancellationToken)
        {
            BaseResponseDto<string> outPut = new BaseResponseDto<string>();
            Borrowing borrowed = await _borrowingRepo.GetByReturnCode(request.returnCode);
            if (borrowed != null)
            {
                borrowed.ReturnDate = request.returnDate;
                calculateAndSetFee(borrowed);
                await _uow.SaveChangesAsync();

                outPut.Message = "ثبت برگشت کتاب با موفقیت انجام شد";
                outPut.Success = true;
                outPut.StatusCode = HttpStatusCode.OK;
                outPut.Data = $"تاریخ برگشت کتاب{borrowed.ReturnDate}";
                return outPut;
            }

            outPut.Message = "کتابی قرض گرفته نشده";
            outPut.Success = true;
            outPut.StatusCode = HttpStatusCode.OK;
            outPut.Data = $"تاریخ برگشت کتاب{borrowed?.ReturnDate}";
            return outPut;

        }

        private void calculateAndSetFee(Borrowing borrowed)
        {
            if (!borrowed.ReturnDate.HasValue) return;
            DateTime expectedReturnDate = borrowed.BorrowDate.AddDays(borrowed.MaxBorrowDay);
            DateTime actualReturnDate = borrowed.ReturnDate.Value;
            if (actualReturnDate <= expectedReturnDate)
            {
                borrowed.lateFee = 0;
                borrowed.TotalLateFee = 0;
                return;
            }

            int lateDays = (actualReturnDate - expectedReturnDate).Days;
            borrowed.lateFee = 2000;
            borrowed.TotalLateFee = lateDays * borrowed.lateFee.Value;
        }
    }
}
