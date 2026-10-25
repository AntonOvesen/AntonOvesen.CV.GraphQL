using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntonOvesen.CV.GraphQL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFoundedAddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Founded",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Companies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Companies");

            migrationBuilder.AddColumn<DateTime>(
                name: "Founded",
                table: "Companies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
