using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Borrowings.Request.Command
{
    public record ReturnBorrowCommand(DateTime returnDate,Guid returnCode) : IRequest<BaseResponseDto<string>>
    {
    }
}
