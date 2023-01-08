using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NowEvent.Data.Migrations
{
    public partial class PlaceId_in_EventAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "EventAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "EventAddress");
        }
    }
}
