using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Features.Borrowings.Request.Query
{
    public class DownloadBorrowHistoryQuery : IRequest<BaseResponseDto<List<BorrowingBackUpDto>>>
    {
    }
}
