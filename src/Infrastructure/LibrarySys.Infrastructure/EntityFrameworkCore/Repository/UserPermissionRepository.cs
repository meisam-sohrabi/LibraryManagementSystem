using LibrarySys.Application.Common.Interfaces.Infrastructure.UserPermissionContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class UserPermissionRepository : GenericRepository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(AppDbContext context) : base(context) { }

    }
}
