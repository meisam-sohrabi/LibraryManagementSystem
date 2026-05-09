using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces.BookAuthorService;
using LibrarySys.Application.Features.Books.Request.Query;
using System.Net;
namespace LibrarySys.Application.Features.Books.Handler.Query
{
    public class GetAllBookSpQueryHandler : IRequestHandler<GetAllBookSpQuery, BaseResponseDto<IReadOnlyList<GetBookResponseDto>>>
    {
        private readonly IBookAuthorService _bookAuthorService;

        public GetAllBookSpQueryHandler(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }
        public async Task<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>> Handle(GetAllBookSpQuery request, CancellationToken cancellationToken)
        {
            var output = new BaseResponseDto<IReadOnlyList<GetBookResponseDto>>();
            var books = await _bookAuthorService.GetBookAuthor();
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
