using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingProject.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Property_PropertyId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "BasePricePerDay",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Room",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priceid",
                table: "Room",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BasePrice = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_Priceid",
                table: "Room",
                column: "Priceid");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Price_Priceid",
                table: "Room",
                column: "Priceid",
                principalTable: "Price",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Property_PropertyId",
                table: "Room",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Price_Priceid",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Property_PropertyId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropIndex(
                name: "IX_Room_Priceid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Priceid",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Room",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<double>(
                name: "BasePricePerDay",
                table: "Room",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Property_PropertyId",
                table: "Room",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id");
        }
    }
}
