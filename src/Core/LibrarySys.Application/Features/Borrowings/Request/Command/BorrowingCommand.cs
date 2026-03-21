using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Borrowings.Request.Command
{
    public class BorrowingCommand : IRequest<BaseResponseDto<string>>
    {
        public SetBorrowRequestDto setBorrow {get;set;}
    }
}
