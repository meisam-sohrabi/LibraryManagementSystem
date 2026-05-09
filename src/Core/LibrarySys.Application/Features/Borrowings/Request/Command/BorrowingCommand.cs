using LibrarySys.Application.Common.DTOs;
namespace LibrarySys.Application.Features.Borrowings.Request.Command
{
    public class BorrowingCommand : IRequest<BaseResponseDto<string>>
    {
        public SetBorrowRequestDto setBorrow {get;set;}
    }
}
