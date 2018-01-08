using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class POWitThinAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "currentThingId",
                table: "Thing",
                type: "int4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "currentThingId",
                table: "ProductionOrders",
                type: "int4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentThingId",
                table: "Thing");

            migrationBuilder.DropColumn(
                name: "currentThingId",
                table: "ProductionOrders");
        }
    }
}
