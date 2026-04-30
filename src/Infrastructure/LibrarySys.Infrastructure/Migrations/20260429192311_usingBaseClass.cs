using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usingBaseClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Identity",
                table: "UserSession",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "UserSession",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Identity",
                table: "UserPermission",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "UserPermission",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Identity",
                table: "Permission",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "Permission",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "member",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "member",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "borrow",
                table: "Borrowings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "borrow",
                table: "Borrowings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "book",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "book",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "book",
                table: "BookAuthor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "book",
                table: "BookAuthor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "author",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "author",
                table: "Authors",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "UserSession");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "UserSession");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "UserPermission");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "UserPermission");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "member",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "member",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "borrow",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "borrow",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "book",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "book",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "book",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "book",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "author",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "author",
                table: "Authors");
        }
    }
}
