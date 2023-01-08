using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NowEvent.Data.Migrations
{
    public partial class EventAddress_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "EventAddress");

            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "EventAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "EventAddress");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "EventAddress",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "EventAddress",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "EventAddress",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
