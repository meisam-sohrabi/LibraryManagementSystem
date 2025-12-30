namespace LibrarySys.Application.Contract
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
