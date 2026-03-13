using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class book_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "book",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "book",
                table: "Books");
        }
    }
}
