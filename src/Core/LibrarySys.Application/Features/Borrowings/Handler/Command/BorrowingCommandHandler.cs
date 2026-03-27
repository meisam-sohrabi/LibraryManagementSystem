using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.Infrastructure.BookContract;
using LibrarySys.Application.Contract.Infrastructure.BorrowingContract;
using LibrarySys.Application.Contract.Infrastructure.MemberContract;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Borrowings.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using System.Net;
namespace LibrarySys.Application.Features.Borrowings.Handler.Command
{
    public class BorrowingCommandHandler : IRequestHandler<BorrowingCommand, BaseResponseDto<string>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _uow;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public BorrowingCommandHandler(IMemberRepository memberRepository, IBorrowingRepository borrowingRepository,
            IBookRepository bookRepository, IUnitOfWork uow)
        {
            _memberRepository = memberRepository;
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _uow = uow;
        }
        public async Task<BaseResponseDto<string>> Handle(BorrowingCommand request, CancellationToken cancellationToken)
        {

            // نکته قرض های فعال با یونیک کردن کلید کتاب از race condition جلوگیری کرد
            // نکته قرض های فعال با لاک سمافور یا لاک خود ترانزاکشن از race condition جلوگیری کرد
            await _semaphore.WaitAsync();
            try
            {
                await _uow.BeginTransactionAsync();
                var output = new BaseResponseDto<string>();

                try
                {


                    if (!request.setBorrow.Id.Any())
                    {
                        output.Message = "مشکل در ارسال داده";
                        output.StatusCode = HttpStatusCode.BadRequest;
                        output.Success = false;
                        return output;
                    }
                    var memberExist = await _memberRepository.GetMember(request.setBorrow.Email);
                    if (memberExist == null)
                    {
                        output.Message = "شما هنوز عضو سامانه نشده اید،ابتدا باید ثبت عضو کنید";
                        output.StatusCode = HttpStatusCode.BadRequest;
                        output.Success = false;
                        return output;
                    }
                    var isBorrowed = false;
                    var borrowings = new List<Borrowing>();

                    foreach (var b in request.setBorrow.Id)
                    {
                       var bookExist = await _bookRepository.ExistById(b);
                        if (!bookExist)
                        {
                            continue;
                        }
                        var borrowing = new Borrowing
                        {
                            BorrowDate = DateTime.UtcNow,
                            MemberId = memberExist.Id,
                            BookId = b,
                            ReturnCode = Guid.NewGuid(),
                        };
                        isBorrowed = await _borrowingRepository.isBorrowedAsync(b);
                        if (isBorrowed)
                        {
                            continue;
                        }
                        borrowings.Add(borrowing);
                    }

                    if (!borrowings.Any())
                    {
                        output.Message = "هیچ کتابی قابل امانت نبود";
                        output.Success = false;
                        return output;
                    }


                    await _borrowingRepository.AddRangeAsync(borrowings);
                    await _uow.SaveChangesAsync();
                    await _uow.CommitTransactionAsync();
                    output.Message = "امانت کتاب ها باموفقیت ثبت شد";
                    output.StatusCode = HttpStatusCode.OK;
                    output.Success = true;
                    output.Data = "کتاب ها با موفقیت ثبت امانت شدند";
                    return output;
                }
                catch (Exception ex)
                {
                    await _uow.RollBackTransactionAsync();
                    output.Message = "متاسفانه خطایی رخ داد کتابی برای امانت ثبت نشد";
                    output.StatusCode = HttpStatusCode.InternalServerError;
                    output.Success = false;
                    return output;
                }

            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
