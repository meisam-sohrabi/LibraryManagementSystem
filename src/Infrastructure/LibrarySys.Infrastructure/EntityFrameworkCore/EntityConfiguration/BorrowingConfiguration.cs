using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    internal class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
    {
        public void Configure(EntityTypeBuilder<Borrowing> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.HasIndex(c => c.BookId).IsUnique().HasFilter($"{nameof(Borrowing.ReturnDate)} IS NULL");
            builder.ToTable("Borrowings", "borrow");
            builder.Property(c => c.ReturnDate).IsRequired(required: false);
            builder.Property(c => c.BookId).IsRequired(required:true);
            builder.Property(c => c.MaxBorrowDay).IsRequired(required:true).HasDefaultValue(14);
            builder.HasOne(c => c.Book)
                .WithMany(c => c.Borrowings)
                .HasForeignKey(c => c.BookId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        }
    }
}
