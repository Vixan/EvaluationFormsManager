﻿// <auto-generated />
using EvaluationFormsManager.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EvaluationFormsManager.Persistence.EF.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EvaluationFormsManager.Domain.Criteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("SectionId");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.EvaluationScale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EvaluationScales");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.EvaluationScaleOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("EvaluationScaleId");

                    b.Property<string>("Name");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationScaleId");

                    b.ToTable("EvaluationScaleOptions");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int?>("ImportanceId");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("ImportanceId");

                    b.HasIndex("StatusId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Importance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Importances");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<int?>("EvaluationScaleId");

                    b.Property<int?>("FormId");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationScaleId");

                    b.HasIndex("FormId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Criteria", b =>
                {
                    b.HasOne("EvaluationFormsManager.Domain.Section")
                        .WithMany("Criteria")
                        .HasForeignKey("SectionId");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.EvaluationScaleOption", b =>
                {
                    b.HasOne("EvaluationFormsManager.Domain.EvaluationScale", "EvaluationScale")
                        .WithMany("EvaluationScaleOptions")
                        .HasForeignKey("EvaluationScaleId");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Form", b =>
                {
                    b.HasOne("EvaluationFormsManager.Domain.Importance", "Importance")
                        .WithMany()
                        .HasForeignKey("ImportanceId");

                    b.HasOne("EvaluationFormsManager.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("EvaluationFormsManager.Domain.Section", b =>
                {
                    b.HasOne("EvaluationFormsManager.Domain.EvaluationScale", "EvaluationScale")
                        .WithMany()
                        .HasForeignKey("EvaluationScaleId");

                    b.HasOne("EvaluationFormsManager.Domain.Form")
                        .WithMany("Sections")
                        .HasForeignKey("FormId");
                });
#pragma warning restore 612, 618
        }
    }
}
