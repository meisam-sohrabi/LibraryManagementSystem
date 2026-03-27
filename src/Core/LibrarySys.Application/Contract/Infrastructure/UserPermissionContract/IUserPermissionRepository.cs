using LibrarySys.Application.Contract.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure.UserPermissionContract
{
    public interface IUserPermissionRepository : IGenericRepositry<UserPermission>
    {
        //Task AssignPermissionToUser(UserPermission userPermissoin);
        //void RevokePermissionFromUser(UserPermission userPermissoin);
    }
}
