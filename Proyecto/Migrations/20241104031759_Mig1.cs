using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Fecha_Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "Civil",
                columns: table => new
                {
                    IdCivil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Casado = table.Column<bool>(type: "bit", nullable: false),
                    Hijos = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Civil", x => x.IdCivil);
                    table.ForeignKey(
                        name: "FK_Civil_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Legal",
                columns: table => new
                {
                    IdLegal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Denuncias = table.Column<bool>(type: "bit", nullable: false),
                    Antecedentes_Penales = table.Column<bool>(type: "bit", nullable: false),
                    Fraudes = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legal", x => x.IdLegal);
                    table.ForeignKey(
                        name: "FK_Legal_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SRI",
                columns: table => new
                {
                    IdSRI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Trabajo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ingresos_Mensuales = table.Column<int>(type: "int", nullable: false),
                    Deudas_Activas = table.Column<bool>(type: "bit", nullable: false),
                    Bienes = table.Column<string>(type: "nvarchar(max)", maxLength: 1000000000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SRI", x => x.IdSRI);
                    table.ForeignKey(
                        name: "FK_SRI_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Civil_IdPersona",
                table: "Civil",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Legal_IdPersona",
                table: "Legal",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_SRI_IdPersona",
                table: "SRI",
                column: "IdPersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Civil");

            migrationBuilder.DropTable(
                name: "Legal");

            migrationBuilder.DropTable(
                name: "SRI");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
