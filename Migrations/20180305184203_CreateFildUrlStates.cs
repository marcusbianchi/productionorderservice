using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class CreateFildUrlStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "measurementUnit",
                table: "PhaseProducts",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "ConfiguredStates",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "ConfiguredStates");

            migrationBuilder.AlterColumn<string>(
                name: "measurementUnit",
                table: "PhaseProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }
    }
}
