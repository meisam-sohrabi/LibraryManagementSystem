using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Borrowings.Request.Query;
using MediatR;
using System.Net;

namespace LibrarySys.Application.Features.Borrowings.Handler.Query
{

    public class DownloadBorrowHistoryQueryHandler : IRequestHandler<DownloadBorrowHistoryQuery, BaseResponseDto<FileStreamDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBorrowingRepository _borrowingRepository;

        public DownloadBorrowHistoryQueryHandler(IMemberRepository memberRepository, IBorrowingRepository borrowingRepository)
        {
            _memberRepository = memberRepository;
            _borrowingRepository = borrowingRepository;
        }
        public async Task<BaseResponseDto<FileStreamDto>> Handle(DownloadBorrowHistoryQuery request, CancellationToken cancellationToken)
        {
            var output = new BaseResponseDto<FileStreamDto>();
            var memberExist = await _memberRepository.GetMember(request.Email);
            if (memberExist == null)
            {
                output.Success = false;
                output.Message = "عضو یافت نشد";
                output.StatusCode = HttpStatusCode.NotFound;
                return output;
            }
            var backupData = await _borrowingRepository.GetByMemberIdAsync(memberExist.Id);
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
                            BookTitle = c.Book.Title ?? "اسم کتاب یافت نشد",
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
            var stream = ExportToJson(backupDto);
            output.Message = "دیتا با موفقت نسخه پشتیبان گرفته شد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = stream;
            return output;

        }

        private FileStreamDto ExportToJson(List<BorrowingBackUpDto> backupData)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(backupData);
            var fileStreamDto = new FileStreamDto
            {
                stream = System.Text.Encoding.UTF8.GetBytes(json),
                Type = "application/json",
                FileName = $"BorrowHistory_{DateTime.Now}.json"
            };
            return fileStreamDto;
        }
    }
}
