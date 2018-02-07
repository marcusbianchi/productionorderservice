using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class CreateFieldsInPhaseProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "PhaseProducts");

            migrationBuilder.AddColumn<int>(
                name: "phaseId",
                table: "Phases",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "measurementUnit",
                table: "PhaseProducts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<double>(
                name: "maxValue",
                table: "PhaseProducts",
                type: "float8",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "minValue",
                table: "PhaseProducts",
                type: "float8",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phaseId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "maxValue",
                table: "PhaseProducts");

            migrationBuilder.DropColumn(
                name: "minValue",
                table: "PhaseProducts");

            migrationBuilder.AlterColumn<string>(
                name: "measurementUnit",
                table: "PhaseProducts",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "PhaseProducts",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
