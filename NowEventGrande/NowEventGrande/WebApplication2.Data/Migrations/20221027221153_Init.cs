using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClientAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAddress_Clients_ClientAddressId",
                        column: x => x.ClientAddressId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DecorationPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventAddress_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ClientId", "ContractorId", "Date", "Size" },
                values: new object[] { 1, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Small" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ClientId", "ContractorId", "Date", "Size" },
                values: new object[] { 2, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Large" });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Email", "EventId", "FirstName", "LastName" },
                values: new object[] { 1, "john@gmail.com", 1, "John", "Doe" });

            migrationBuilder.CreateIndex(
                name: "IX_Budget_EventId",
                table: "Budget",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddress_ClientAddressId",
                table: "ClientAddress",
                column: "ClientAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventAddress_EventId",
                table: "EventAddress",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_EventId",
                table: "Guests",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "ClientAddress");

            migrationBuilder.DropTable(
                name: "EventAddress");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
