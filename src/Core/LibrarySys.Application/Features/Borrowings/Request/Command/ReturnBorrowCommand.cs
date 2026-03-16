using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Borrowings.Request.Command
{
    public class ReturnBorrowCommand : IRequest<BaseResponseDto<string>>
    {
        public ReturnBorrowRequestDto ReturnBorrow { get; set; }
    }
}
