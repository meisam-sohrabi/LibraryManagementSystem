using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using System.Net;

namespace LibrarySys.Application.Features.Books.Handler.Command
{
    public class BookImageCommandHandler : IRequestHandler<BookImageCommand, BaseResponseDto<string>>
    {
        private readonly IBookRepository _bookRepository;

        public BookImageCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
            outPut.Message = $"تصویر مورد نظر برای کتاب {bookExist.Title} ثبت شد";
            outPut.StatusCode = HttpStatusCode.OK;
            outPut.Success = true;
            outPut.Data = request.ImageUrl;
            return outPut;

        }
    }
}
