using LibrarySys.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace LibrarySys.Identity.Entity
{
    public class CustomUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
