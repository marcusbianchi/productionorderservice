using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class StateConfigurationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateConfigurations",
                columns: table => new
                {
                    stateConfigurationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    productionOrderTypeId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateConfigurations", x => x.stateConfigurationId);
                    table.ForeignKey(
                        name: "FK_StateConfigurations_ProductionOrderTypes_productionOrderTypeId",
                        column: x => x.productionOrderTypeId,
                        principalTable: "ProductionOrderTypes",
                        principalColumn: "productionOrderTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguredStates",
                columns: table => new
                {
                    configuredStateId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    possibleNextStates = table.Column<string[]>(type: "text[]", nullable: true),
                    state = table.Column<string>(type: "text", nullable: true),
                    stateConfigurationId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguredStates", x => x.configuredStateId);
                    table.ForeignKey(
                        name: "FK_ConfiguredStates_StateConfigurations_stateConfigurationId",
                        column: x => x.stateConfigurationId,
                        principalTable: "StateConfigurations",
                        principalColumn: "stateConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguredStates_stateConfigurationId",
                table: "ConfiguredStates",
                column: "stateConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_StateConfigurations_productionOrderTypeId",
                table: "StateConfigurations",
                column: "productionOrderTypeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguredStates");

            migrationBuilder.DropTable(
                name: "StateConfigurations");
        }
    }
}
