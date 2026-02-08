using LibrarySys.Application.Contract.BookAuthorService;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Books.Request.Query;
using MediatR;
using System.Net;
namespace LibrarySys.Application.Features.Books.Handler.Query
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BaseResponseDto<BookAuthorRequestDto>>
    {
        private readonly IBookAuthorService _bookAuthorService;

        public GetBookQueryHandler(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }
        public async Task<BaseResponseDto<BookAuthorRequestDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var output = new BaseResponseDto<BookAuthorRequestDto>();

            var book = await _bookAuthorService.GetByIdWithAuthor(request.Id);
            if (book == null)
            {
                output.Message = "کتاب یافت نشد";
                output.StatusCode = HttpStatusCode.NotFound;
                output.Success = false;
                return output;
            }
            output.Message = "کتاب یافت شد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = book;
            return output;

        }
    }
}
