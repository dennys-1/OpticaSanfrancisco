using Microsoft.EntityFrameworkCore.Migrations;

namespace OpticaSanfrancisco.Migrations
{
    public partial class LAPICITO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "t_product",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "t_product");
        }
    }
}
