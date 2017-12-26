using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace productionorderservice.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionOrderTypes",
                columns: table => new
                {
                    productionOrderTypeId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    typeDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    typeScope = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrderTypes", x => x.productionOrderTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    enabled = table.Column<bool>(type: "bool", nullable: false),
                    parentProductsIds = table.Column<int[]>(type: "int4[]", nullable: true),
                    productCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    productDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    productGTIN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    productName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "ThingGroups",
                columns: table => new
                {
                    thingGroupId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    groupCode = table.Column<string>(type: "text", nullable: true),
                    groupName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingGroups", x => x.thingGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInformations",
                columns: table => new
                {
                    additionalInformationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Information = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    productId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInformations", x => x.additionalInformationId);
                    table.ForeignKey(
                        name: "FK_AdditionalInformations_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    tagDescription = table.Column<string>(type: "text", nullable: true),
                    tagName = table.Column<string>(type: "text", nullable: true),
                    thingGroupId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.tagId);
                    table.ForeignKey(
                        name: "FK_Tags_ThingGroups_thingGroupId",
                        column: x => x.thingGroupId,
                        principalTable: "ThingGroups",
                        principalColumn: "thingGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    recipeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recipeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recipeProductphaseProductId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.recipeId);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    phaseId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    phaseCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    phaseName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    recipeId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.phaseId);
                    table.ForeignKey(
                        name: "FK_Phases_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrders",
                columns: table => new
                {
                    productionOrderId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    productionOrderNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    productionOrderTypeId = table.Column<int>(type: "int4", nullable: false),
                    recipeId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.productionOrderId);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_ProductionOrderTypes_productionOrderTypeId",
                        column: x => x.productionOrderTypeId,
                        principalTable: "ProductionOrderTypes",
                        principalColumn: "productionOrderTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhaseParameters",
                columns: table => new
                {
                    phaseParameterId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    maxValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    measurementUnit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    minValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    phaseId = table.Column<int>(type: "int4", nullable: true),
                    setupValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    tagId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseParameters", x => x.phaseParameterId);
                    table.ForeignKey(
                        name: "FK_PhaseParameters_Phases_phaseId",
                        column: x => x.phaseId,
                        principalTable: "Phases",
                        principalColumn: "phaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhaseProducts",
                columns: table => new
                {
                    phaseProductId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    measurementUnit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    phaseId = table.Column<int>(type: "int4", nullable: true),
                    phaseProductType = table.Column<int>(type: "int4", nullable: false),
                    productId = table.Column<int>(type: "int4", nullable: false),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseProducts", x => x.phaseProductId);
                    table.ForeignKey(
                        name: "FK_PhaseProducts_Phases_phaseId",
                        column: x => x.phaseId,
                        principalTable: "Phases",
                        principalColumn: "phaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInformations_productId",
                table: "AdditionalInformations",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseParameters_phaseId",
                table: "PhaseParameters",
                column: "phaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_phaseId",
                table: "PhaseProducts",
                column: "phaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_recipeId",
                table: "Phases",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_productionOrderTypeId",
                table: "ProductionOrders",
                column: "productionOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_recipeId",
                table: "ProductionOrders",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_recipeProductphaseProductId",
                table: "Recipes",
                column: "recipeProductphaseProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_thingGroupId",
                table: "Tags",
                column: "thingGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_PhaseProducts_recipeProductphaseProductId",
                table: "Recipes",
                column: "recipeProductphaseProductId",
                principalTable: "PhaseProducts",
                principalColumn: "phaseProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhaseProducts_Phases_phaseId",
                table: "PhaseProducts");

            migrationBuilder.DropTable(
                name: "AdditionalInformations");

            migrationBuilder.DropTable(
                name: "PhaseParameters");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductionOrderTypes");

            migrationBuilder.DropTable(
                name: "ThingGroups");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "PhaseProducts");
        }
    }
}
