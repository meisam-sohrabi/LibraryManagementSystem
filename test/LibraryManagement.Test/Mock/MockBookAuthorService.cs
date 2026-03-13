using LibrarySys.Application.Contract.BookAuthorService;
using LibrarySys.Application.DTOs;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockBookAuthorService
    {
        public static Mock<IBookAuthorService> BookAuthroSer()
        {

            var books = new BookAuthorRequestDto
            {
                Title = "The Great Gatsby",
                Genere = "Fiction",
                PublishYear = 1925,
                AvailableCopies = 5,
                Authors = new List<AuthorRequestAppDto>
                {
                    new AuthorRequestAppDto
                    {
                        Name = "F. Scott Fitzgerald",
                        BirthYear = 1896
                    }

                }
            };




            var mockSer = new Mock<IBookAuthorService>();
            mockSer.Setup(c => c.GetByIdWithAuthor(It.IsAny<Guid>())).ReturnsAsync(books);

            return mockSer;
        }
    }
}
