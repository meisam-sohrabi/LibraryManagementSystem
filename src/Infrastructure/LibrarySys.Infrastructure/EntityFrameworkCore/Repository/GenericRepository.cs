using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class GenericRepository<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbset;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await _dbset.AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task<T> Get(Guid id)
        {
           return await _dbset.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbset.AsQueryable();
        }

        //public async Task<T> Get(Guid id)
        //{
        //    return await _dbset.FindAsync(id);
        //}

        //public Task<List<T>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(T entity)
        {
            var entry = _dbset.Entry(entity);
            var key = _dbset.EntityType.Model.FindEntityType(typeof(T))?.FindPrimaryKey();
            if (key != null)
            {
                foreach (var property in key.Properties)
                {
                    entry.Property(property.Name).IsModified = false;
                }
            }
        }
    }
}
