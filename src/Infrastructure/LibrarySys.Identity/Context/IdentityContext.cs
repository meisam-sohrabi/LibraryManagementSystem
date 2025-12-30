using LibrarySys.Identity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Identity.Context
{
    internal class IdentityContext : IdentityDbContext<CustomUser>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }


    }
}
