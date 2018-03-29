using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class CorrectionOnModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxValue",
                table: "PhaseParameters");

            migrationBuilder.DropColumn(
                name: "minValue",
                table: "PhaseParameters");

            migrationBuilder.AlterColumn<string>(
                name: "measurementUnit",
                table: "PhaseParameters",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "measurementUnit",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maxValue",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "minValue",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: true);
        }
    }
}
