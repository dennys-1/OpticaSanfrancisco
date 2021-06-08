using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OpticaSanfrancisco.Migrations
{
    public partial class NUEVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Validacion");

            migrationBuilder.DropTable(
                name: "Tipo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Deshabilitado = table.Column<bool>(type: "boolean", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Validacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    APELLIDO = table.Column<string>(type: "text", nullable: true),
                    EMAIL = table.Column<string>(type: "text", nullable: true),
                    IDTipo = table.Column<int>(type: "integer", nullable: false),
                    NOMBRE = table.Column<string>(type: "text", nullable: true),
                    N_DOCUMENTO = table.Column<int>(type: "integer", nullable: false),
                    OPCIONES = table.Column<int>(type: "integer", nullable: false),
                    TELEFONO = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    TipoID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Validacion_Tipo_TipoID",
                        column: x => x.TipoID,
                        principalTable: "Tipo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Validacion_TipoID",
                table: "Validacion",
                column: "TipoID");
        }
    }
}
