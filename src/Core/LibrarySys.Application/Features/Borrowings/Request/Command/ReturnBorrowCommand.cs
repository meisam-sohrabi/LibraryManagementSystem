using LibrarySys.Application.Common.DTOs;

namespace LibrarySys.Application.Features.Borrowings.Request.Command
{
    public class ReturnBorrowCommand : IRequest<BaseResponseDto<string>>
    {
        public ReturnBorrowRequestDto ReturnBorrow { get; set; }
    }
}
