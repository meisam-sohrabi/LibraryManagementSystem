using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookId_Tborrowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrowings_BookId",
                schema: "borrow",
                table: "Borrowings");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_BookId",
                schema: "borrow",
                table: "Borrowings",
                column: "BookId",
                unique: true,
                filter: "ReturnDate IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrowings_BookId",
                schema: "borrow",
                table: "Borrowings");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_BookId",
                schema: "borrow",
                table: "Borrowings",
                column: "BookId");
        }
    }
}
