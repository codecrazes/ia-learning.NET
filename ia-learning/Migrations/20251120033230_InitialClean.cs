using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ia_learning.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IA_HABILIDADE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_HABILIDADE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IA_MODELO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Provedor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Custo = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_MODELO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IA_USUARIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IA_AVALIACAO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nota = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Comentario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IAId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_AVALIACAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IA_AVALIACAO_IA_MODELO_IAId",
                        column: x => x.IAId,
                        principalTable: "IA_MODELO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IA_AVALIACAO_IA_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "IA_USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IA_RECOMENDACAO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Mensagem = table.Column<string>(type: "NCLOB", nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_RECOMENDACAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IA_RECOMENDACAO_IA_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "IA_USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IA_TAREFA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Dificuldade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TempoDisponivelMin = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataExecucao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IAId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_TAREFA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IA_TAREFA_IA_MODELO_IAId",
                        column: x => x.IAId,
                        principalTable: "IA_MODELO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IA_TAREFA_IA_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "IA_USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IA_USUARIO_HABILIDADE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    HabilidadeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IA_USUARIO_HABILIDADE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IA_USUARIO_HABILIDADE_IA_HABILIDADE_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "IA_HABILIDADE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IA_USUARIO_HABILIDADE_IA_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "IA_USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IA_AVALIACAO_IAId",
                table: "IA_AVALIACAO",
                column: "IAId");

            migrationBuilder.CreateIndex(
                name: "IX_IA_AVALIACAO_UsuarioId",
                table: "IA_AVALIACAO",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_IA_RECOMENDACAO_UsuarioId",
                table: "IA_RECOMENDACAO",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_IA_TAREFA_IAId",
                table: "IA_TAREFA",
                column: "IAId");

            migrationBuilder.CreateIndex(
                name: "IX_IA_TAREFA_UsuarioId",
                table: "IA_TAREFA",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_IA_USUARIO_HABILIDADE_HabilidadeId",
                table: "IA_USUARIO_HABILIDADE",
                column: "HabilidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_IA_USUARIO_HABILIDADE_UsuarioId",
                table: "IA_USUARIO_HABILIDADE",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IA_AVALIACAO");

            migrationBuilder.DropTable(
                name: "IA_RECOMENDACAO");

            migrationBuilder.DropTable(
                name: "IA_TAREFA");

            migrationBuilder.DropTable(
                name: "IA_USUARIO_HABILIDADE");

            migrationBuilder.DropTable(
                name: "IA_MODELO");

            migrationBuilder.DropTable(
                name: "IA_HABILIDADE");

            migrationBuilder.DropTable(
                name: "IA_USUARIO");
        }
    }
}
