using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusApi.Migrations
{
    /// <inheritdoc />
    public partial class AlignWithDiagram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Asignacion_Estudiante_Curso",
                table: "Asignacion");

            migrationBuilder.CreateIndex(
                name: "IX_Asignacion_EstudianteId",
                table: "Asignacion",
                column: "EstudianteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Asignacion_EstudianteId",
                table: "Asignacion");

            migrationBuilder.CreateIndex(
                name: "UQ_Asignacion_Estudiante_Curso",
                table: "Asignacion",
                columns: new[] { "EstudianteId", "CursoId" },
                unique: true);
        }
    }
}

