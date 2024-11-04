using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class Migracion20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SRI_IdPersona",
                table: "SRI");

            migrationBuilder.DropIndex(
                name: "IX_Legal_IdPersona",
                table: "Legal");

            migrationBuilder.DropIndex(
                name: "IX_Civil_IdPersona",
                table: "Civil");

            migrationBuilder.CreateTable(
                name: "Solicitud",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_Solicitud_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SRI_IdPersona",
                table: "SRI",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Legal_IdPersona",
                table: "Legal",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Civil_IdPersona",
                table: "Civil",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_IdPersona",
                table: "Solicitud",
                column: "IdPersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_SRI_IdPersona",
                table: "SRI");

            migrationBuilder.DropIndex(
                name: "IX_Legal_IdPersona",
                table: "Legal");

            migrationBuilder.DropIndex(
                name: "IX_Civil_IdPersona",
                table: "Civil");

            migrationBuilder.CreateIndex(
                name: "IX_SRI_IdPersona",
                table: "SRI",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Legal_IdPersona",
                table: "Legal",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Civil_IdPersona",
                table: "Civil",
                column: "IdPersona");
        }
    }
}
