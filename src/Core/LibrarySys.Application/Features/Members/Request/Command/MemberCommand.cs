using LibrarySys.Application.DTOs;
using LibrarySys.Domain.Entity;
using MediatR;

namespace LibrarySys.Application.Features.Members.Request.Command
{
    public class MemberCommand : IRequest<BaseResponseDto<Member>>
    {
        public string Email { get; set; }
    }
}
