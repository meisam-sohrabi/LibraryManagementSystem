using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Application.Common.Interfaces.Infrastructure.BookContract;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Domain.Entity;
using System.Net;

namespace LibrarySys.Application.Features.Books.Handler.Command
{
    public class BookImageCommandHandler : IRequestHandler<BookImageCommand, BaseResponseDto<string>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookImageCommandHandler(IBookRepository bookRepository,IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseDto<string>> Handle(BookImageCommand request, CancellationToken cancellationToken)
        {
            BaseResponseDto<string> outPut = new BaseResponseDto<string>();
            Book bookExist = await _bookRepository.Get(request.BookId);
            if (bookExist == null)
            {
                outPut.Message = "کتاب مورد نظر یافت نشد";
                outPut.StatusCode = HttpStatusCode.NotFound;
                outPut.Success = false;
                outPut.Data = request.ImageUrl;
                return outPut;
            }
            bookExist.ImageUrl = request.ImageUrl;
            _bookRepository.Update(bookExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            outPut.Message = $"تصویر مورد نظر برای کتاب {bookExist.Title} ثبت شد";
            outPut.StatusCode = HttpStatusCode.OK;
            outPut.Success = true;
            outPut.Data = request.ImageUrl;
            return outPut;

        }
    }
}
