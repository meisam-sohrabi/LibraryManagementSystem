using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(c=> c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.ToTable("Books", "book");
            builder.Property(c=> c.Title).HasMaxLength(250).IsRequired(required:true);
            builder.Property(c => c.Genere).HasMaxLength(70).IsRequired(required:true);
        }
    }
}
