using Microsoft.EntityFrameworkCore.Migrations;

namespace OpticaSanfrancisco.Migrations
{
    public partial class productos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "t_product",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "t_product");
        }
    }
}
