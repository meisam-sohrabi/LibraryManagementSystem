using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Domain.Entity;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockBookAuthRepository
    {
        public static Mock<IBookRepository> BookAuthRepo()
        {
            var books = new List<Book>
            {

             new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Great Gatsby",
                Genere = "Fiction",
                PublishYear = 1925,
                AvailableCopies = 5,
                Authors = new List<BookAuthor>
                {
                     new BookAuthor
                    {
                        Author = new Author
                        {
                            Id = Guid.NewGuid(),
                            Name = "F. Scott Fitzgerald",
                            BirthYear = 1896
                        }
                    }
                }
            },
             new Book
            {
                Id = Guid.NewGuid(),
                Title = "To Kill a Mockingbird",
                Genere = "Fiction",
                PublishYear = 1960,
                AvailableCopies = 3,
                Authors = new List<BookAuthor>
                {
                    new BookAuthor
                    {
                        Author = new Author
                        {
                            Id = Guid.NewGuid(),
                            Name = "Harper Lee",
                            BirthYear = 1926
                        }
                    }
                }
            }
          };

            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(books);


            mockRepo.Setup(r => r.AddAsync(It.IsAny<Book>())).Returns((Book book) =>
            {
                books.Add(book);
                return Task.FromResult(book);
            });

            return mockRepo;
        }
    }
}
