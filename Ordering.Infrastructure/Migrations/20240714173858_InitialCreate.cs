using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    total_pricae = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    address_line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    state = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    zip_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    card_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    card_number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    card_type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    expiration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    cvv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    payment_method = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "orderdetail",
                columns: table => new
                {
                    oderdetail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderdetail", x => x.oderdetail_id);
                    table.ForeignKey(
                        name: "FK_orderdetail_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderdetail_order_id",
                table: "orderdetail",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderdetail");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
