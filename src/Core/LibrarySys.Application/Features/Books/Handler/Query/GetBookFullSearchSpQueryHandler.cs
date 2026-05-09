using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces.BookAuthorService;
using LibrarySys.Application.Features.Books.Request.Query;
using System.Net;

namespace LibrarySys.Application.Features.Books.Handler.Query
{
    public class GetBookFullSearchSpQueryHandler : IRequestHandler<GetBookFullSearchSpQuery, BaseResponseDto<IEnumerable<BookAuthorFullSearchFilterResponseDto>>>
    {
        private readonly IBookAuthorService _bookAuthorService;

        public GetBookFullSearchSpQueryHandler(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }
        public async Task<BaseResponseDto<IEnumerable<BookAuthorFullSearchFilterResponseDto>>> Handle(GetBookFullSearchSpQuery request, CancellationToken cancellationToken)
        {
            var output = new BaseResponseDto<IEnumerable<BookAuthorFullSearchFilterResponseDto>>();
            var books = await _bookAuthorService.FullSearch(request.fullSearch);
            if (!books.Any())
            {
                output.Message = "لیستی موجود نمی باشد";
                output.StatusCode = HttpStatusCode.NotFound;
                output.Success = false;
                return output;
            }
            output.Message = "لیست با موفقیت دریافت شد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = books;
            return output;
        }
    }
}
