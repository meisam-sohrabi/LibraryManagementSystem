using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Application.Common.Interfaces.Infrastructure.PermissionContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.UserContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.UserPermissionContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Domain.Enum;
using Quartz;
using System.Threading;
namespace LibrarySys.Infrastructure.Jobs
{
    public class SeedDataJob : IJob
    {
        private readonly IUserManagerRepository _userManagerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public SeedDataJob(IUserManagerRepository userManagerRepo,
            IUnitOfWork unitOfWork,
            IPermissionRepository permissionRepository,
            IUserPermissionRepository userPermissionRepository)
        {
            _userManagerRepo = userManagerRepo;
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await SeedAdmin();
        }

        private async Task SeedAdmin()
        {
            // try catch is necessary here 
            // alway use loging like start ... now end


            await _unitOfWork.BeginTransactionAsync();

            // 1. Create admin
            try
            {

                bool adminExist = await _userManagerRepo.ExistById("0d60b040-a17f-432e-81d2-a5193cbd4a72");
                if (adminExist)
                {
                    Console.WriteLine("Admin already exist...");
                    return;
                }
                User admin = new User
                {
                    Id = "0d60b040-a17f-432e-81d2-a5193cbd4a72",
                    FullName = "admin",
                    UserName = "Admin_a",
                    Email = null,
                    PhoneNumber = null,
                    Role = UserRole.Admin,
                    UserStatus = UserActivity.Active,
                    PasswordHash = _userManagerRepo.PasswordHash("Admin@123")

                };
                await _userManagerRepo.CreateAsync(admin);
                // 2. Create permission

                Permission permission = new Permission
                {
                    Id = new Guid("a3eee975-e521-473f-b55d-a1c3cd3b75b0"),
                    Resource = "BookController",
                    Action = "Add",
                    Description = "Permission for adding a book"
                };

                await _permissionRepository.AddAsync(permission);

                // 3. Assign a permission to a user

                UserPermission userPermission = new UserPermission
                {
                    UserId = "0d60b040-a17f-432e-81d2-a5193cbd4a72",
                    User = admin,
                    PermissionId = new Guid("a3eee975-e521-473f-b55d-a1c3cd3b75b0"),
                    Permission = permission
                };

                await _userPermissionRepository.AddAsync(userPermission);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync();
                Console.WriteLine($"An error happend:{ex.Message}");
            }

        }
    }

}
