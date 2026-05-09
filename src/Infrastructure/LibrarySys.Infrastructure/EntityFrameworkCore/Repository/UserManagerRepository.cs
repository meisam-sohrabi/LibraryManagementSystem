using BCrypt.Net;
using LibrarySys.Application.Common.Interfaces.Infrastructure.UserContract;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly AppDbContext _context;

        public UserManagerRepository(AppDbContext context)
        {
            _context = context;
        }

        //public Task<bool> ChangePasswordAsync(User user, string password)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task CreateAsync(User user)
        {

            await _context.AddAsync(user);
        }

        public async Task<bool> ExistByEmail(string email)
        {
            return await _context.User.AnyAsync(c => c.Email == email);
        }

        public async Task<bool> ExistById(string id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExistByUsername(string username)
        {
            return await _context.User.AnyAsync(c=> c.UserName == username);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.User.FirstOrDefaultAsync(c=> c.UserName == username);
        }

        public string PasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void Update(User user)
        {
             _context.User.Update(user);
        }

        //public Task<bool> ResetPasswordAsync(User user, string newPassword)
        //{
        //    throw new NotImplementedException();
        //}

        public bool VerifyPassword(string password, User user)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}
