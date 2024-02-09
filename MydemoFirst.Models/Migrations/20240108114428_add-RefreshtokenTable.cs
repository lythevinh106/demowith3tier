using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MydemoFirst.Models.Migrations
{
    public partial class addRefreshtokenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79640b64-94d3-4cb2-89c8-a5fefe3c2051",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "41f4a8e9-0a87-42c4-87bb-7631e62fbb17", "7b7f034d-c8d9-45dc-b9b6-fceedd62b190" });

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 },
                column: "Quantity",
                value: 48);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 2 },
                column: "Quantity",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 3 },
                column: "Quantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 4 },
                column: "Quantity",
                value: 38);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 5, 5 },
                column: "Quantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 6 },
                column: "Quantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 7 },
                column: "Quantity",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 8 },
                column: "Quantity",
                value: 48);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 9 },
                column: "Quantity",
                value: 36);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 10 },
                column: "Quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 11, 11 },
                column: "Quantity",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 12, 12 },
                column: "Quantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 13, 13 },
                column: "Quantity",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 14, 14 },
                column: "Quantity",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 15, 15 },
                column: "Quantity",
                value: 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79640b64-94d3-4cb2-89c8-a5fefe3c2051",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8a34e1b7-9518-4cb5-8e66-f7e052f4d008", "eac3d73f-7cc7-4d97-a06f-86042375fd99" });

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 },
                column: "Quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 2 },
                column: "Quantity",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 3 },
                column: "Quantity",
                value: 31);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 4 },
                column: "Quantity",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 5, 5 },
                column: "Quantity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 6 },
                column: "Quantity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 7 },
                column: "Quantity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 8 },
                column: "Quantity",
                value: 47);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 9 },
                column: "Quantity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 10 },
                column: "Quantity",
                value: 43);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 11, 11 },
                column: "Quantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 12, 12 },
                column: "Quantity",
                value: 46);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 13, 13 },
                column: "Quantity",
                value: 37);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 14, 14 },
                column: "Quantity",
                value: 49);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 15, 15 },
                column: "Quantity",
                value: 34);
        }
    }
}
