using LibrarySys.Application.Common.Interfaces.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Common.Interfaces.Infrastructure.UserPermissionContract
{
    public interface IUserPermissionRepository : IGenericRepositry<UserPermission>
    {
        //Task AssignPermissionToUser(UserPermission userPermissoin);
        //void RevokePermissionFromUser(UserPermission userPermissoin);
    }
}
