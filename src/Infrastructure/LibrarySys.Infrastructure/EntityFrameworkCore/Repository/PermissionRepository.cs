using LibrarySys.Application.Contract.Infrastructure.PermissionContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context)
        {
        }
        
    }
}
