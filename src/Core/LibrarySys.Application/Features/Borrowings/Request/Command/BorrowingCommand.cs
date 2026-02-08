using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Borrowings.Request.Command
{
    public class BorrowingCommand : IRequest<BaseResponseDto<string>>
    {
        public List<Guid> Id { get; set; }
        public string Email { get; set; }
    }
}
