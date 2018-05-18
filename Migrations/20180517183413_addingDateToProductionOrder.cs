using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class addingDateToProductionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "latestUpdate",
                table: "ProductionOrders",
                type: "int8",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_HistStates_productionOrderId",
                table: "HistStates",
                column: "productionOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistStates_ProductionOrders_productionOrderId",
                table: "HistStates",
                column: "productionOrderId",
                principalTable: "ProductionOrders",
                principalColumn: "productionOrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistStates_ProductionOrders_productionOrderId",
                table: "HistStates");

            migrationBuilder.DropIndex(
                name: "IX_HistStates_productionOrderId",
                table: "HistStates");

            migrationBuilder.DropColumn(
                name: "latestUpdate",
                table: "ProductionOrders");
        }
    }
}
