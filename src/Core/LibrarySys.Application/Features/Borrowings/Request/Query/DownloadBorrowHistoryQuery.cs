using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Borrowings.Request.Query
{
    public class DownloadBorrowHistoryQuery : IRequest<BaseResponseDto<FileStreamDto>>
    {
        public string Email { get; set; }
    }
}
