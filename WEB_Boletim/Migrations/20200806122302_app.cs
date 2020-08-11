using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_Boletim.Migrations
{
    public partial class app : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoFaculdade = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Situacao",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SituacaoMateria = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situacao", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAluno = table.Column<string>(nullable: false),
                    SobrenomeAluno = table.Column<string>(nullable: false),
                    DataNascimentoAluno = table.Column<DateTime>(nullable: false),
                    CPFAluno = table.Column<string>(nullable: false),
                    CursoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Aluno_Curso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoMateria = table.Column<string>(nullable: false),
                    DataDeCadastroMateria = table.Column<DateTime>(nullable: false),
                    SituacaoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Materia_Situacao_SituacaoID",
                        column: x => x.SituacaoID,
                        principalTable: "Situacao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nota",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoID = table.Column<int>(nullable: false),
                    MateriaID = table.Column<int>(nullable: false),
                    NotaAluno = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nota_Aluno_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Aluno",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nota_Materia_MateriaID",
                        column: x => x.MateriaID,
                        principalTable: "Materia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_CursoID",
                table: "Aluno",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_SituacaoID",
                table: "Materia",
                column: "SituacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_AlunoID",
                table: "Nota",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_MateriaID",
                table: "Nota",
                column: "MateriaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nota");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Situacao");
        }
    }
}
