using LibrarySys.Application.Common.Interfaces.Infrastructure.BorrowingContract;
using LibrarySys.Domain.Entity;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockBorrowingRepository
    {
        public static Mock<IBorrowingRepository> BorrowingRepo()
        {
            var borrowings = new List<Borrowing>
            {
                new Borrowing
                {
                    BorrowDate = DateTime.UtcNow,
                    BookId = Guid.NewGuid(),
                    MemberId = Guid.NewGuid(),
                },
                  new Borrowing
                {
                    BorrowDate = DateTime.UtcNow,
                    BookId = Guid.NewGuid(),
                    MemberId = Guid.NewGuid(),
                },
            };


            var mockRepo = new Mock<IBorrowingRepository>();
            mockRepo.Setup(c => c.AddRangeAsync(It.IsAny<List<Borrowing>>())).Returns((List<Borrowing> borrowing) =>
            {
                borrowings.AddRange(borrowing);
                return Task.FromResult(borrowing);
            });
            return mockRepo;
        }
    }
}
