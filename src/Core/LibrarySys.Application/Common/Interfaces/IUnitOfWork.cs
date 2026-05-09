namespace LibrarySys.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        Task BeginTransactionAsync();
        Task SaveCommit();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
