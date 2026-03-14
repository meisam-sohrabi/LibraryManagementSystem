using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class borrow_maxborrowday_fee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxBorrowDay",
                schema: "borrow",
                table: "Borrowings",
                type: "int",
                nullable: false,
                defaultValue: 14);

            migrationBuilder.AddColumn<int>(
                name: "TotalLateFee",
                schema: "borrow",
                table: "Borrowings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "lateFee",
                schema: "borrow",
                table: "Borrowings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxBorrowDay",
                schema: "borrow",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "TotalLateFee",
                schema: "borrow",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "lateFee",
                schema: "borrow",
                table: "Borrowings");
        }
    }
}
