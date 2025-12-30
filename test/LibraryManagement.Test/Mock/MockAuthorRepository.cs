using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Domain.Entity;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockAuthorRepository
    {
        public static Mock<IAuthorRepository> AuthorRepo()
        {
            //var authorsName = new List<string>
            //{
            //    "F. Scott Fitzgerald" ,"Harper Lee"
            //};

            var authors = new List<Author>
            {
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "F. Scott Fitzgerald",
                    BirthYear = 1896
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Harper Lee",
                    BirthYear = 1926
                }
            };
            var mockRepo = new Mock<IAuthorRepository>();
            mockRepo.Setup(c => c.GetAuthorByName(It.IsAny<List<string>>())).ReturnsAsync(authors);
            return mockRepo;
        }
    }
}
