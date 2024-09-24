using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogComunitario.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AccountCreationDate",
                table: "IdentityUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "IdentityUser",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "IdentityUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "IdentityUser",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "IdentityUser",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "IdentityUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IdentityUser",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "IdentityUser",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCreationDate",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "IdentityUser");
        }
    }
}
