using LibraryManagement.Test.Mock;
using LibrarySys.Application.Common.Interfaces.Infrastructure.BookContract;
using Moq;
using Shouldly;

namespace LibraryManagement.Test.BookAuthorService.Query
{
    public class GetAllBookAuthorsQueryHandlerTest
    {
        Mock<IBookRepository> _mock;
        public GetAllBookAuthorsQueryHandlerTest()
        {
            _mock = MockBookAuthRepository.BookAuthRepo();
        }


        [Fact]
        public async Task Book_Get_ShouldReturn_Book()
        {
            var books = await _mock.Object.GetAll();
            books.Count().ShouldBe(2);
        }
    }
}
