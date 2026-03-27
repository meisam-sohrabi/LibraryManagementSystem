using LibrarySys.Application.Contract.Infrastructure.MemberContract;
using LibrarySys.Domain.Entity;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockMemberRepository
    {
        public static Mock<IMemberRepository> MockMemberRepo()
        {
            var member = new Member
            {
                Email = "Alber@yahoo.com",
                JoinDate = DateTime.UtcNow
            };


            var mockRepo = new Mock<IMemberRepository>();
            mockRepo.Setup(c => c.GetMember(It.IsAny<string>())).ReturnsAsync(member);
            return mockRepo;
        }
    }
}
