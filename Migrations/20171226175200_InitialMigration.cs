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
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    productCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    productDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    productGTIN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    productId = table.Column<int>(type: "int4", nullable: false),
                    productName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.internalId);
                });

            migrationBuilder.CreateTable(
                name: "ThingGroups",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    groupCode = table.Column<string>(type: "text", nullable: true),
                    groupName = table.Column<string>(type: "text", nullable: true),
                    thingGroupId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingGroups", x => x.internalId);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInformations",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Information = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ProductinternalId = table.Column<int>(type: "int4", nullable: true),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    additionalInformationId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInformations", x => x.internalId);
                    table.ForeignKey(
                        name: "FK_AdditionalInformations_Products_ProductinternalId",
                        column: x => x.ProductinternalId,
                        principalTable: "Products",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    tagDescription = table.Column<string>(type: "text", nullable: true),
                    tagId = table.Column<int>(type: "int4", nullable: false),
                    tagName = table.Column<string>(type: "text", nullable: true),
                    thingGroupId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.internalId);
                    table.ForeignKey(
                        name: "FK_Tags_ThingGroups_thingGroupId",
                        column: x => x.thingGroupId,
                        principalTable: "ThingGroups",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    recipeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recipeId = table.Column<int>(type: "int4", nullable: false),
                    recipeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recipeProductinternalId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.internalId);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RecipeinternalId = table.Column<int>(type: "int4", nullable: true),
                    phaseCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    phaseId = table.Column<int>(type: "int4", nullable: false),
                    phaseName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.internalId);
                    table.ForeignKey(
                        name: "FK_Phases_Recipes_RecipeinternalId",
                        column: x => x.RecipeinternalId,
                        principalTable: "Recipes",
                        principalColumn: "internalId",
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
                    quantity = table.Column<int>(type: "int4", nullable: false),
                    recipeinternalId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.productionOrderId);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Recipes_recipeinternalId",
                        column: x => x.recipeinternalId,
                        principalTable: "Recipes",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhaseParameters",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PhaseinternalId = table.Column<int>(type: "int4", nullable: true),
                    maxValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    measurementUnit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    minValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    setupValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    tagId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseParameters", x => x.internalId);
                    table.ForeignKey(
                        name: "FK_PhaseParameters_Phases_PhaseinternalId",
                        column: x => x.PhaseinternalId,
                        principalTable: "Phases",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhaseProducts",
                columns: table => new
                {
                    internalId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PhaseinternalId = table.Column<int>(type: "int4", nullable: true),
                    measurementUnit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    phaseProductId = table.Column<int>(type: "int4", nullable: false),
                    phaseProductType = table.Column<int>(type: "int4", nullable: false),
                    productId = table.Column<int>(type: "int4", nullable: false),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseProducts", x => x.internalId);
                    table.ForeignKey(
                        name: "FK_PhaseProducts_Phases_PhaseinternalId",
                        column: x => x.PhaseinternalId,
                        principalTable: "Phases",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhaseProducts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "internalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInformations_ProductinternalId",
                table: "AdditionalInformations",
                column: "ProductinternalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseParameters_PhaseinternalId",
                table: "PhaseParameters",
                column: "PhaseinternalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_PhaseinternalId",
                table: "PhaseProducts",
                column: "PhaseinternalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_productId",
                table: "PhaseProducts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_RecipeinternalId",
                table: "Phases",
                column: "RecipeinternalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_productionOrderNumber",
                table: "ProductionOrders",
                column: "productionOrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_recipeinternalId",
                table: "ProductionOrders",
                column: "recipeinternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_recipeProductinternalId",
                table: "Recipes",
                column: "recipeProductinternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_thingGroupId",
                table: "Tags",
                column: "thingGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_PhaseProducts_recipeProductinternalId",
                table: "Recipes",
                column: "recipeProductinternalId",
                principalTable: "PhaseProducts",
                principalColumn: "internalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhaseProducts_Products_productId",
                table: "PhaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PhaseProducts_Phases_PhaseinternalId",
                table: "PhaseProducts");

            migrationBuilder.DropTable(
                name: "AdditionalInformations");

            migrationBuilder.DropTable(
                name: "PhaseParameters");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.DropTable(
                name: "ProductionOrderTypes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ThingGroups");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "PhaseProducts");
        }
    }
}
