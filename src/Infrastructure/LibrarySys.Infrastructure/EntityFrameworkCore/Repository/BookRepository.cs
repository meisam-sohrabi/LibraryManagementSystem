using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Exist(string Title,string Genre)
        {
            return await _context.Book.Where(c => c.Title == Title && c.Genere == Genre).AnyAsync();
        }

        
    }
}
