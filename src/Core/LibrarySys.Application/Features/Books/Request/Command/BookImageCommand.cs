using LibrarySys.Application.DTOs;
using MediatR;

namespace LibrarySys.Application.Features.Books.Request.Command
{
    public record BookImageCommand : IRequest<BaseResponseDto<string>>
    {
        public Guid BookId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
