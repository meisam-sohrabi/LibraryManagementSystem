using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Application.Features.Books.Request.Query;
using LibrarySysApi.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Add")]
        public async Task<BaseResponseDto<BookAuthorRequestDto>> CreateBook([FromBody] BookAuthorRequestDto bookAuthorRequest)
        {
            var command = new BookCommand { BookAuthorRequest = bookAuthorRequest };
            return await _mediator.Send(command);
        }

        [HttpGet("GetAll")]
        [Permission()]
        //[Authorize()]
        public async Task<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllBookSpQuery());
        }

        [HttpGet("Get")]
        public async Task<BaseResponseDto<BookAuthorRequestDto>> Get(Guid Id)
        {
            var query = new GetBookQuery(Id);
            return await _mediator.Send(query);
        }
    }
}
