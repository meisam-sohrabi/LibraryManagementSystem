//using LibrarySys.Domain.Entity;
//using LibrarySys.Domain.Enum;
//using Microsoft.EntityFrameworkCore;

//namespace LibrarySys.Infrastructure.Extentions
//{
//    public static class ModelBuilderExtention
//    {
//        public static void Seed(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.SeedUserWithPermission();
//        }

//        private static void SeedUserWithPermission(this ModelBuilder modelBuilder)
//        {
//            User admin = new User
//            {
//                Id = "0d60b040-a17f-432e-81d2-a5193cbd4a72",
//                FullName = "admin",
//                UserName = "Admin_a",
//                Email = null,
//                PhoneNumber = null,
//                Role = UserRole.Admin,
//                UserStatus = UserActivity.Active,

//            };

//            admin.PasswordHash = PasswordHash("Admin@123");

//            modelBuilder.Entity<User>().HasData(admin);

//            Permission permission = new Permission
//            {
//                Id = new Guid("a3eee975-e521-473f-b55d-a1c3cd3b75b0"),
//                Resource = "BookController",
//                Action = "Add",
//                Description = "Permission for adding a book"
//            };
//            modelBuilder.Entity<Permission>().HasData(permission);

//            UserPermission userPermission = new UserPermission
//            {
//                UserId = "0d60b040-a17f-432e-81d2-a5193cbd4a72",
//                PermissionId = new Guid("a3eee975-e521-473f-b55d-a1c3cd3b75b0"),
//            };

//            modelBuilder.Entity<UserPermission>().HasData(userPermission);
//        }

//        private static string PasswordHash(string password)
//        {
//            return BCrypt.Net.BCrypt.HashPassword(password);
//        }
//    }
//}
