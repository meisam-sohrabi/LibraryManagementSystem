using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces.FileService;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Application.Features.Books.Request.Query;
using LibrarySysApi.Attributes;
using LibrarySysApi.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LibrarySysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMemoryCache _cashe;
        public BookController(IMediator mediator, IFileStorageService fileStorageService,IMemoryCache cashe)
        {
            _mediator = mediator;
            _fileStorageService = fileStorageService;
            _cashe = cashe;
        }
        [HttpPost("Add")]
        public async Task<BaseResponseDto<BookAuthorRequestDto>> CreateBook([FromBody] BookAuthorRequestDto bookAuthorRequest)
        {
            var command = new BookCommand { BookAuthorRequest = bookAuthorRequest };
            return await _mediator.Send(command);
        }

        [HttpPost("UploadImage")]
        [Authorize]
        public async Task<BaseResponseDto<string>> UploadBookImage([FromForm] UploadBookImageDto ImageRequest)
        {
            string? bookUrl = null;
            if (ImageRequest.ImageFile != null)
            {
                using var stream = ImageRequest.ImageFile.OpenReadStream();
                bookUrl = await _fileStorageService.SaveBookImageAsync(stream, ImageRequest.ImageFile.FileName);
            }
            var command = new BookImageCommand { BookId = ImageRequest.BookId, ImageUrl = bookUrl };
            return await _mediator.Send(command);
        }

        [HttpGet("GetAll")]
        [Permission()]
        [Authorize()]
        public async Task<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>> GetAll()
        {
            string casheKey = "Books-Key";
            if(!_cashe.TryGetValue(casheKey, out BaseResponseDto<IReadOnlyList<GetBookResponseDto>> response))
            {
                response = await _mediator.Send(new GetAllBookSpQuery());
                if(response != null)
                {
                    _cashe.Set(casheKey, response, TimeSpan.FromMinutes(2));
                }
            }
            return response ?? new BaseResponseDto<IReadOnlyList<GetBookResponseDto>>();
        }

        [HttpGet("Get")]
        [Authorize()]
        public async Task<BaseResponseDto<BookAuthorRequestDto>> Get(Guid Id)
        {
            var query = new GetBookQuery(Id);
            return await _mediator.Send(query);
        }

        [HttpPost("FullSearch")]
        [Authorize()]
        public async Task<BaseResponseDto<IEnumerable<BookAuthorFullSearchFilterResponseDto>>> FullSearch(BookAuthorFullSearchRequestDto search)
        {
            var command = new GetBookFullSearchSpQuery { fullSearch = search };
            return await _mediator.Send(command); ;
        }
    }
}
