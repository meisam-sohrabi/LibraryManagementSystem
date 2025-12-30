using LibraryManagement.Test.Mock;
using LibrarySys.Application.Contract.BookAuthor;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Handlers.Books.Handler.Query;
using LibrarySys.Application.Features.Handlers.Books.Request.Query;
using Moq;
using Shouldly;

namespace LibraryManagement.Test.BookAuthorService.Query
{
    public class GetBookAuthorsQueryHandlerTest
    {
        Mock<IBookAuthorService> _mock;
        public GetBookAuthorsQueryHandlerTest()
        {
            _mock = MockBookAuthorService.BookAuthroSer();
        }


        [Fact]
        public async Task BookAuthor_Get_ShouldReturn_Book()
        {
            var handler = new GetBookQueryHandler(_mock.Object);
            var result = await handler.Handle(new GetBookQuery(Guid.NewGuid()),CancellationToken.None);

            result.ShouldBeOfType<BaseResponseDto<BookAuthorRequestDto>>();
        }
    }
}
