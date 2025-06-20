using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AT_TourManager.Migrations
{
    /// <inheritdoc />
    public partial class DeletarReservas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reservas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reservas");
        }
    }
}
