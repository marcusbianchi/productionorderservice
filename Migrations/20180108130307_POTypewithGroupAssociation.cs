using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class POTypewithGroupAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "thingGroupIds",
                table: "ProductionOrderTypes",
                type: "integer[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Thing",
                columns: table => new
                {
                    thingId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ThingGroupinternalId = table.Column<int>(type: "int4", nullable: true),
                    thingCode = table.Column<string>(type: "text", nullable: true),
                    thingName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thing", x => x.thingId);
                    table.ForeignKey(
                        name: "FK_Thing_ThingGroups_ThingGroupinternalId",
                        column: x => x.ThingGroupinternalId,
                        principalTable: "ThingGroups",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thing_ThingGroupinternalId",
                table: "Thing",
                column: "ThingGroupinternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thing");

            migrationBuilder.DropColumn(
                name: "thingGroupIds",
                table: "ProductionOrderTypes");
        }
    }
}
