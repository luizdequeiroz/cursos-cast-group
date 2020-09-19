using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace app.Migrations
{
    public partial class CursosCastGroup001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Assunto = table.Column<string>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataTermino = table.Column<DateTime>(nullable: false),
                    QuantidadeAlunos = table.Column<int>(nullable: false),
                    CodigoCategoria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Categorias_CodigoCategoria",
                        column: x => x.CodigoCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "RegisterDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Comportamental", new DateTime(2020, 9, 19, 17, 14, 56, 695, DateTimeKind.Local).AddTicks(7707), null },
                    { 2, "Programação", new DateTime(2020, 9, 19, 17, 14, 56, 697, DateTimeKind.Local).AddTicks(1202), null },
                    { 3, "Qualidade", new DateTime(2020, 9, 19, 17, 14, 56, 697, DateTimeKind.Local).AddTicks(1251), null },
                    { 4, "Processos", new DateTime(2020, 9, 19, 17, 14, 56, 697, DateTimeKind.Local).AddTicks(1254), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_CodigoCategoria",
                table: "Cursos",
                column: "CodigoCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
