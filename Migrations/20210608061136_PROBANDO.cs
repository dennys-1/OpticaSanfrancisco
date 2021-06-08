using Microsoft.EntityFrameworkCore.Migrations;

namespace OpticaSanfrancisco.Migrations
{
    public partial class PROBANDO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "t_proforma",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "t_proforma",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "t_proforma");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "t_proforma");
        }
    }
}
