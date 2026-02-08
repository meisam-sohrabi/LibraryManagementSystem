//using LibraryManagement.Test.Mock;
//using LibrarySys.Application.Contract.Infrastructure;
//using LibrarySys.Application.DTOs;
//using LibrarySys.Application.Features.Borrowings.Handler.Command;
//using LibrarySys.Application.Features.Borrowings.Request.Command;
//using Moq;
//using Shouldly;

//namespace LibraryManagement.Test.BorrowingService.Command
//{
//    public class BorrowingCommandHandlerTest
//    {
//        Mock<IBorrowingRepository> _mock;
//        Mock<IMemberRepository> _mockMember;
//        public BorrowingCommandHandlerTest()
//        {
//            _mock = MockBorrowingRepository.BorrowingRepo();
//            _mockMember = MockMemberRepository.MockMemberRepo();
//        }


//        [Fact]
//        public async Task Borrowing_Create_ShouldReturn_String()
//        {
//            var handler = new BorrowingCommandHandler(_mockMember.Object, _mock.Object);
//            var booksId = new List<Guid> { Guid.NewGuid(),Guid.NewGuid() };
//            var result = await handler.Handle(new BorrowingCommand { Email = "Alber@yahoo.com", Id = booksId }, CancellationToken.None);
//            result.ShouldBeOfType<BaseResponseDto<string>>();
//            result.ShouldNotBeNull();
//        }
//    }
//}
