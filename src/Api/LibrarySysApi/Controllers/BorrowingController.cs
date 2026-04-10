using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Borrowings.Request.Command;
using LibrarySys.Application.Features.Borrowings.Request.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibrarySysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Borrow")]
        [Authorize(Roles = "User")]
        public async Task<BaseResponseDto<string>> SetBorrow([FromBody] List<Guid> Books)
        {
            SetBorrowRequestDto borrow = new SetBorrowRequestDto
            {
                Email = User.FindFirstValue(ClaimTypes.Email),
                Id = Books
            };
            var command = new BorrowingCommand { setBorrow = borrow };
            return await _mediator.Send(command);
        }


        [HttpPost("ReturnBorrow")]
        [Authorize()]
        public async Task<BaseResponseDto<string>> ReturnBorrow(ReturnBorrowRequestDto returnBorrow)
        { 
            var command = new ReturnBorrowCommand { ReturnBorrow = returnBorrow };
            return await _mediator.Send(command);
        }



        [HttpGet("download-my-history")]
        [Authorize(Roles = "User")]
        public async Task<BaseResponseDto<List<BorrowingBackUpDto>>> DownloadBackup()
        {
            var command = new DownloadBorrowHistoryQuery();
            return await _mediator.Send(command);
          
        }


    }
}
