using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF.Migrations
{
    public partial class DataContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationScale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationScale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Importance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationScaleOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    EvaluationScaleId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationScaleOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationScaleOption_EvaluationScale_EvaluationScaleId",
                        column: x => x.EvaluationScaleId,
                        principalTable: "EvaluationScale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImportanceId = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Importance_ImportanceId",
                        column: x => x.ImportanceId,
                        principalTable: "Importance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EvaluationScaleId = table.Column<int>(nullable: true),
                    FormId = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_EvaluationScale_EvaluationScaleId",
                        column: x => x.EvaluationScaleId,
                        principalTable: "EvaluationScale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Section_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criteria_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criteria_SectionId",
                table: "Criteria",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScaleOption_EvaluationScaleId",
                table: "EvaluationScaleOption",
                column: "EvaluationScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_ImportanceId",
                table: "Forms",
                column: "ImportanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_EvaluationScaleId",
                table: "Section",
                column: "EvaluationScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_FormId",
                table: "Section",
                column: "FormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropTable(
                name: "EvaluationScaleOption");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "EvaluationScale");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Importance");
        }
    }
}
