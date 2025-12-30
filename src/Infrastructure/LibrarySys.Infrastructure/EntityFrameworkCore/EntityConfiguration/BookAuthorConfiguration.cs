using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(c => new { c.AuthorId, c.BookId });
            builder.ToTable("BookAuthor", "book");
            builder.HasOne(c => c.Book)
                .WithMany(c => c.Authors)
                .HasForeignKey(c => c.BookId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            builder.HasOne(c => c.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

        }
    }
}
