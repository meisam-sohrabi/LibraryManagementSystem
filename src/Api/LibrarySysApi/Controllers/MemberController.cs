using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Features.Members.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Add")]
        [Authorize()]
        public async Task<BaseResponseDto<Member>> CreateMember(string Email)
        {
            var command = new MemberCommand { Email = Email };
            return await _mediator.Send(command);
        }
    }
}
