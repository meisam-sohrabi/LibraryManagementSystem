using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Common.Interfaces.Infrastructure.UserContract
{
    public interface IUserManagerRepository 
    {
        Task CreateAsync(User user);
        Task<User> FindByIdAsync(string id);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUsernameAsync(string username);
        Task<bool> ExistByUsername(string username);
        Task<bool> ExistById(string id);
        Task<bool> ExistByEmail(string email);
        void Update(User user);
        //Task<bool> ChangePasswordAsync(User user, string password);
        //Task<bool> ResetPasswordAsync(User user, string newPassword);
        string PasswordHash(string data);
        bool VerifyPassword(string password,User user);
    }
}
