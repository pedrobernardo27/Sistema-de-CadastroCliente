using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroClienteRepository.Migrations
{
    /// <inheritdoc />
    public partial class BancoAlterado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cpf",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cpf",
                table: "Clientes");
        }
    }
}
