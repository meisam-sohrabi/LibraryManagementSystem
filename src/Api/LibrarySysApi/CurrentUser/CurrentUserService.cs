using LibrarySys.Application.Contract.CurrentUser;
using System.Security.Claims;

namespace LibrarySysApi.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        private readonly string? _userId;
        private readonly string _email;
        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _userId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _email = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";
        }

        public string? UserId => _userId; // once it initialized in ctor so anytime we need to use it we had request one time.
        public string Email => _email;

        // but here everytime when we want to access we need to check httpcontext
        public string IPAddress { get => _httpContext.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? ""; }



    }

}

