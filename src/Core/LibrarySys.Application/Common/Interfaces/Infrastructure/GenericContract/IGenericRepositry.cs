namespace LibrarySys.Application.Common.Interfaces.Infrastructure.GenericContract
{
    public interface IGenericRepositry<T>
    {
        Task<T> Get(Guid id);
        Task<List<T>> GetAll();
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
