using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Book {  get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<Borrowing> Borrowing { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
