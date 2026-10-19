using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using CampusApi.Data;

#nullable disable

namespace CampusApi.Migrations;

[DbContext(typeof(AcademicDbContext))]
[Migration("20261018000000_AddUsuarios")]
public partial class AddUsuarios : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Usuario",
            columns: table => new
            {
                UsuarioId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Usuario_Correo",
            table: "Usuario",
            column: "Correo",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Usuario");
    }
}


