﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using productionorderservice.Data;
using productionorderservice.Model;
using System;

namespace productionorderservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("productionorderservice.Model.AdditionalInformation", b =>
                {
                    b.Property<int>("internalId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Information")
                        .HasMaxLength(50);

                    b.Property<int?>("ProductinternalId");

                    b.Property<string>("Value")
                        .HasMaxLength(50);

                    b.Property<int>("additionalInformationId");

                    b.HasKey("internalId");

                    b.HasIndex("ProductinternalId");

                    b.ToTable("AdditionalInformations");
                });

            modelBuilder.Entity("productionorderservice.Model.ConfiguredState", b =>
                {
                    b.Property<int>("configuredStateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string[]>("possibleNextStates");

                    b.Property<string>("state");

                    b.Property<int?>("stateConfigurationId");

                    b.Property<string>("url");

                    b.HasKey("configuredStateId");

                    b.HasIndex("stateConfigurationId");

                    b.ToTable("ConfiguredStates");
                });

            modelBuilder.Entity("productionorderservice.Model.Phase", b =>
                {
                    b.Property<int>("internalId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("RecipeinternalId");

                    b.Property<string>("phaseCode")
                        .HasMaxLength(100);

                    b.Property<int>("phaseId");

                    b.Property<string>("phaseName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("internalId");

                    b.HasIndex("RecipeinternalId");

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("productionorderservice.Model.PhaseParameter", b =>
                {
                    b.Property<int>("internalId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PhaseinternalId");

                    b.Property<string>("maxValue")
                        .HasMaxLength(50);

                    b.Property<string>("measurementUnit")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("minValue")
                        .HasMaxLength(50);

                    b.Property<string>("setupValue")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("internalId");

                    b.HasIndex("PhaseinternalId");

                    b.ToTable("PhaseParameters");
                });

            modelBuilder.Entity("productionorderservice.Model.PhaseProduct", b =>
                {
                    b.Property<int>("internalId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PhaseinternalId");

                    b.Property<double>("maxValue");

                    b.Property<string>("measurementUnit")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("minValue");

                    b.Property<int>("phaseProductType");

                    b.HasKey("internalId");

                    b.HasIndex("PhaseinternalId");

                    b.ToTable("PhaseProducts");
                });

            modelBuilder.Entity("productionorderservice.Model.Product", b =>
                {
                    b.Property<int>("internalId");

                    b.Property<string>("productCode")
                        .HasMaxLength(50);

                    b.Property<string>("productDescription")
                        .HasMaxLength(100);

                    b.Property<string>("productGTIN")
                        .HasMaxLength(50);

                    b.Property<int>("productId");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("internalId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("productionorderservice.Model.ProductionOrder", b =>
                {
                    b.Property<int>("productionOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("currentStatus")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("created");

                    b.Property<int?>("currentThingId");

                    b.Property<string>("productionOrderNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("productionOrderTypeId")
                        .IsRequired();

                    b.Property<int?>("quantity");

                    b.Property<int>("recipeinternalId");

                    b.HasKey("productionOrderId");

                    b.HasIndex("productionOrderNumber");

                    b.HasIndex("recipeinternalId");

                    b.ToTable("ProductionOrders");
                });

            modelBuilder.Entity("productionorderservice.Model.ProductionOrderType", b =>
                {
                    b.Property<int>("productionOrderTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int[]>("thingGroupIds")
                        .HasColumnName("thingGroupIds")
                        .HasColumnType("integer[]");

                    b.Property<string>("typeDescription")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("typeScope")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("productionOrderTypeId");

                    b.ToTable("ProductionOrderTypes");
                });

            modelBuilder.Entity("productionorderservice.Model.Recipe", b =>
                {
                    b.Property<int>("internalId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("recipeCode")
                        .HasMaxLength(50);

                    b.Property<int>("recipeId");

                    b.Property<string>("recipeName")
                        .HasMaxLength(50);

                    b.Property<int?>("recipeProductinternalId");

                    b.HasKey("internalId");

                    b.HasIndex("recipeProductinternalId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("productionorderservice.Model.StateConfiguration", b =>
                {
                    b.Property<int>("stateConfigurationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("productionOrderTypeId");

                    b.HasKey("stateConfigurationId");

                    b.HasIndex("productionOrderTypeId")
                        .IsUnique();

                    b.ToTable("StateConfigurations");
                });

            modelBuilder.Entity("productionorderservice.Model.Tag", b =>
                {
                    b.Property<int>("internalId");

                    b.Property<string>("tagDescription");

                    b.Property<string>("tagGroup");

                    b.Property<int>("tagId");

                    b.Property<string>("tagName");

                    b.Property<int>("thingGroupId");

                    b.HasKey("internalId");

                    b.HasIndex("thingGroupId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("productionorderservice.Model.Thing", b =>
                {
                    b.Property<int>("thingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ThingGroupinternalId");

                    b.Property<int?>("currentThingId");

                    b.Property<string>("thingCode");

                    b.Property<string>("thingName");

                    b.HasKey("thingId");

                    b.HasIndex("ThingGroupinternalId");

                    b.ToTable("Thing");
                });

            modelBuilder.Entity("productionorderservice.Model.ThingGroup", b =>
                {
                    b.Property<int>("internalId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("groupCode");

                    b.Property<string>("groupName");

                    b.Property<int>("thingGroupId");

                    b.HasKey("internalId");

                    b.ToTable("ThingGroups");
                });

            modelBuilder.Entity("productionorderservice.Model.AdditionalInformation", b =>
                {
                    b.HasOne("productionorderservice.Model.Product")
                        .WithMany("additionalInformation")
                        .HasForeignKey("ProductinternalId");
                });

            modelBuilder.Entity("productionorderservice.Model.ConfiguredState", b =>
                {
                    b.HasOne("productionorderservice.Model.StateConfiguration")
                        .WithMany("states")
                        .HasForeignKey("stateConfigurationId");
                });

            modelBuilder.Entity("productionorderservice.Model.Phase", b =>
                {
                    b.HasOne("productionorderservice.Model.Recipe")
                        .WithMany("phases")
                        .HasForeignKey("RecipeinternalId");
                });

            modelBuilder.Entity("productionorderservice.Model.PhaseParameter", b =>
                {
                    b.HasOne("productionorderservice.Model.Phase")
                        .WithMany("phaseParameters")
                        .HasForeignKey("PhaseinternalId");
                });

            modelBuilder.Entity("productionorderservice.Model.PhaseProduct", b =>
                {
                    b.HasOne("productionorderservice.Model.Phase")
                        .WithMany("phaseProducts")
                        .HasForeignKey("PhaseinternalId");
                });

            modelBuilder.Entity("productionorderservice.Model.Product", b =>
                {
                    b.HasOne("productionorderservice.Model.PhaseProduct")
                        .WithOne("product")
                        .HasForeignKey("productionorderservice.Model.Product", "internalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("productionorderservice.Model.ProductionOrder", b =>
                {
                    b.HasOne("productionorderservice.Model.Recipe", "recipe")
                        .WithMany()
                        .HasForeignKey("recipeinternalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("productionorderservice.Model.Recipe", b =>
                {
                    b.HasOne("productionorderservice.Model.PhaseProduct", "recipeProduct")
                        .WithMany()
                        .HasForeignKey("recipeProductinternalId");
                });

            modelBuilder.Entity("productionorderservice.Model.StateConfiguration", b =>
                {
                    b.HasOne("productionorderservice.Model.ProductionOrderType")
                        .WithOne("stateConfiguration")
                        .HasForeignKey("productionorderservice.Model.StateConfiguration", "productionOrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("productionorderservice.Model.Tag", b =>
                {
                    b.HasOne("productionorderservice.Model.PhaseParameter")
                        .WithOne("tag")
                        .HasForeignKey("productionorderservice.Model.Tag", "internalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("productionorderservice.Model.ThingGroup", "thingGroup")
                        .WithMany()
                        .HasForeignKey("thingGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("productionorderservice.Model.Thing", b =>
                {
                    b.HasOne("productionorderservice.Model.ThingGroup")
                        .WithMany("things")
                        .HasForeignKey("ThingGroupinternalId");
                });
#pragma warning restore 612, 618
        }
    }
}
