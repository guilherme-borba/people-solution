using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Api.Migrations
{
    // Essa migration cria a tabela inicial
    public partial class InitialCreate : Migration
    {
        // Método que sobe a migration
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    // ID único para cada pessoa (chave pr.)
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),

                    // Nome da pessoa
                    Nome = table.Column<string>(type: "TEXT", nullable: false),

                    // Sobrenome da pessoa
                    Sobrenome = table.Column<string>(type: "TEXT", nullable: false),

                    // Telefone da pessoa
                    Telefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    // Define o ID como chave primária
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        // Método que desfaz a migration
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
