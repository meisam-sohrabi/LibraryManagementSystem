using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces.CurrentUser;
using LibrarySys.Application.Common.Interfaces.Infrastructure.BorrowingContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.MemberContract;
using LibrarySys.Application.Features.Borrowings.Request.Query;
using System.Net;

namespace LibrarySys.Application.Features.Borrowings.Handler.Query
{

    public class DownloadBorrowHistoryQueryHandler : IRequestHandler<DownloadBorrowHistoryQuery, BaseResponseDto<List<BorrowingBackUpDto>>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IBorrowingRepository _borrowingRepository;

        public DownloadBorrowHistoryQueryHandler(IMemberRepository memberRepository,ICurrentUserService currentUser, IBorrowingRepository borrowingRepository)
        {
            _memberRepository = memberRepository;
            _currentUser = currentUser;
            _borrowingRepository = borrowingRepository;
        }
        public async Task<BaseResponseDto<List<BorrowingBackUpDto>>> Handle(DownloadBorrowHistoryQuery request, CancellationToken cancellationToken)
        {

            Console.WriteLine($"ip address:{_currentUser.IPAddress},Email:{_currentUser.Email},UserId:{_currentUser.UserId}");

            var output = new BaseResponseDto<List<BorrowingBackUpDto>>();
            var memberExist = await _memberRepository.GetMember(_currentUser.Email);
            if (memberExist == null)
            {
                output.Success = false;
                output.Message = "عضو یافت نشد";
                output.StatusCode = HttpStatusCode.NotFound;
                return output;
            }
            var backupData = await _borrowingRepository.GetAllByMemberIdAsync(memberExist.Id);
            var backupDto = backupData.Select(c => new BorrowingBackUpDto
            {
                Meta = new BackUpMeta
                {
                    CreatedAt = DateTime.UtcNow,
                    MemberId = memberExist.Id,

                },
                Borrowings = new List<BorrowingDto>
                    {
                        new BorrowingDto
                        {
                            BookTitle = c.Book?.Title ?? "اسم کتاب یافت نشد",
                            BorrowDate = c.BorrowDate,
                            ReturnDate = c.ReturnDate
                        }
                    }
            }).ToList();
            if (!backupDto.Any())
            {
                output.Message = "هیچ سابقه امانتی برای این عضو یافت نشد";
                output.StatusCode = HttpStatusCode.NotFound;
                output.Success = false;
                return output;
            }
            output.Message = "دیتا با موفقت نسخه پشتیبان گرفته شد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = backupDto;
            return output;

        }

        
    }
}
