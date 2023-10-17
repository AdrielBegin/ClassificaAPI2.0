using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classifica3._0.Migrations
{
    /// <inheritdoc />
    public partial class Inicial_correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCardCreated",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "DataCardUpdated",
                table: "Cards",
                newName: "DateCardCreated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCardUpdated",
                table: "Cards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCardUpdated",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "DateCardCreated",
                table: "Cards",
                newName: "DataCardUpdated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCardCreated",
                table: "Cards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
