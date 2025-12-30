using LibrarySys.Application.Contract;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> UOW()
        {

            var uowRepo = new Mock<IUnitOfWork>();

            uowRepo.Setup(c => c.SaveChangesAsync()).ReturnsAsync(1);

            return uowRepo;
        }
    }
}
