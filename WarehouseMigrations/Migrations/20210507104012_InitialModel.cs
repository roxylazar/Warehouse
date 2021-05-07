using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseMigrations.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Satay Chicken Nuddeln" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Cannelloni al Formaggio" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Dumpling Soup" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "The Salad Bowl" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Lasagne con Pomodoro" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Spinach Salad" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Muesli with Raspberry" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "Nut Butter Balls" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "Popcorn with Raspberry" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "Yogurt Parfait" });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "ExpirationDate", "ProductId", "Quantity" },
                values: new object[] { 1, new DateTime(2021, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10 });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "ExpirationDate", "ProductId", "Quantity" },
                values: new object[] { 4, new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 25 });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "ExpirationDate", "ProductId", "Quantity" },
                values: new object[] { 2, new DateTime(2021, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 50 });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "ExpirationDate", "ProductId", "Quantity" },
                values: new object[] { 3, new DateTime(2021, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
