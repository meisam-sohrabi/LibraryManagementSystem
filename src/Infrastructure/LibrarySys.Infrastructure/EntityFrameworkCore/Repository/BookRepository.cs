using LibrarySys.Application.Common.Interfaces.Infrastructure.BookContract;
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

        public async Task<bool> ExistById(Guid Id)
        {
            return await _context.Book.AnyAsync(c => c.Id == Id);
        }

        public async Task<bool> ExistByTitleAndGenre(string Title,string Genre)
        {
            return await _context.Book.Where(c => c.Title == Title && c.Genere == Genre).AnyAsync();
        }

        
    }
}
