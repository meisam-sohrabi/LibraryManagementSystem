using LibrarySys.Application.Contract.FileService;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Application.Features.Books.Request.Query;
using LibrarySysApi.Attributes;
using LibrarySysApi.DTOs;
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
        private readonly IFileStorageService _fileStorageService;

        public BookController(IMediator mediator, IFileStorageService fileStorageService)
        {
            _mediator = mediator;
            _fileStorageService = fileStorageService;
        }
        [HttpPost("Add")]
        public async Task<BaseResponseDto<BookAuthorRequestDto>> CreateBook([FromBody] BookAuthorRequestDto bookAuthorRequest)
        {

            //string? bookUrl = null;
            //if(bookAuthorRequest.BookImageFile != null)
            //{
            //    using var stream = bookAuthorRequest.BookImageFile.OpenReadStream();
            //    bookUrl =  await _fileStorageService.SaveBookImageAsync(stream, bookAuthorRequest.BookImageFile.FileName);
            //}

            //BookAuthorRequestAppDto bookAuth = new BookAuthorRequestAppDto
            //{
            //    Title = bookAuthorRequest.Title,
            //    Genere = bookAuthorRequest.Genere,
            //    AvailableCopies = bookAuthorRequest.AvailableCopies,
            //    PublishYear = bookAuthorRequest.PublishYear,
            //    BookImageUrl = bookUrl ?? "There is no image uploaded.",
            //    Authors = bookAuthorRequest.Authors.Select(c => new AuthorRequestAppDto
            //    {
            //        Name = c.Name,
            //        BirthYear = c.BirthYear,
            //    }).ToList()
            // };

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
        //[Authorize()]
        public async Task<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllBookSpQuery());
        }

        [HttpGet("Get")]
        public async Task<BaseResponseDto<LibrarySys.Application.DTOs.BookAuthorRequestDto>> Get(Guid Id)
        {
            var query = new GetBookQuery(Id);
            return await _mediator.Send(query);
        }
    }
}
