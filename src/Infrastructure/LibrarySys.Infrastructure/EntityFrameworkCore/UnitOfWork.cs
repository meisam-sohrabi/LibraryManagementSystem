using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveCommit()
        {
            await _context.Database.CommitTransactionAsync();
        }


        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
