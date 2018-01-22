using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class TagMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "internalId",
                table: "Tags",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_PhaseParameters_internalId",
                table: "Tags",
                column: "internalId",
                principalTable: "PhaseParameters",
                principalColumn: "internalId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_PhaseParameters_internalId",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "internalId",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }
    }
}
