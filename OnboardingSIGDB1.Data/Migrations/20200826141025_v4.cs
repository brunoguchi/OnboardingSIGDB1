using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardingSIGDB1.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Funcionarios_FuncionarioId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_FuncionarioId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Empresas");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataContratacao",
                table: "Funcionarios",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Funcionarios",
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Funcionarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Empresas",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFundacao",
                table: "Empresas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresas",
                fixedLength: true,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Cargos",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FuncionariosCargos",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false),
                    CargoId = table.Column<int>(nullable: false),
                    DataDeVinculo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionariosCargos", x => new { x.CargoId, x.FuncionarioId });
                    table.ForeignKey(
                        name: "FK_FuncionariosCargos_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionariosCargos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosCargos_FuncionarioId",
                table: "FuncionariosCargos",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios");

            migrationBuilder.DropTable(
                name: "FuncionariosCargos");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_EmpresaId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataContratacao",
                table: "Funcionarios",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Funcionarios",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Empresas",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFundacao",
                table: "Empresas",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresas",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 14);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Empresas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Cargos",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_FuncionarioId",
                table: "Empresas",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Funcionarios_FuncionarioId",
                table: "Empresas",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
