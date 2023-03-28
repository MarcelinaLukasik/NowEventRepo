using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NowEvent.Data.Migrations
{
    public partial class updateRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Requests");
        }
    }
}
