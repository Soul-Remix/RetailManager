using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailManager.Migrations
{
    public partial class Create_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrductId",
                table: "Inventory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrductId",
                table: "Inventory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
