using LibrarySys.Application.Common.DTOs;
namespace LibrarySys.Application.Features.Books.Request.Query
{
    public class GetAllBookSpQuery : IRequest<BaseResponseDto<IReadOnlyList<GetBookResponseDto>>>
    {
    }
}
