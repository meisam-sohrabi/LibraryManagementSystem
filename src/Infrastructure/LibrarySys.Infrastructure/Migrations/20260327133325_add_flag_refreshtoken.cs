using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_flag_refreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                schema: "Identity",
                table: "RefreshToken",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRevoked",
                schema: "Identity",
                table: "RefreshToken");
        }
    }
}
