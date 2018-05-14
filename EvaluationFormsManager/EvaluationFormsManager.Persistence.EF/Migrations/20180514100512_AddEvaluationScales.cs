using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF.Migrations
{
    public partial class AddEvaluationScales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criteria_Section_SectionId",
                table: "Criteria");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationScaleOption_EvaluationScale_EvaluationScaleId",
                table: "EvaluationScaleOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_EvaluationScale_EvaluationScaleId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Forms_FormId",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationScaleOption",
                table: "EvaluationScaleOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationScale",
                table: "EvaluationScale");

            migrationBuilder.RenameTable(
                name: "Section",
                newName: "Sections");

            migrationBuilder.RenameTable(
                name: "EvaluationScaleOption",
                newName: "EvaluationScaleOptions");

            migrationBuilder.RenameTable(
                name: "EvaluationScale",
                newName: "EvaluationScales");

            migrationBuilder.RenameIndex(
                name: "IX_Section_FormId",
                table: "Sections",
                newName: "IX_Sections_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Section_EvaluationScaleId",
                table: "Sections",
                newName: "IX_Sections_EvaluationScaleId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluationScaleOption_EvaluationScaleId",
                table: "EvaluationScaleOptions",
                newName: "IX_EvaluationScaleOptions_EvaluationScaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationScaleOptions",
                table: "EvaluationScaleOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationScales",
                table: "EvaluationScales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Criteria_Sections_SectionId",
                table: "Criteria",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationScaleOptions_EvaluationScales_EvaluationScaleId",
                table: "EvaluationScaleOptions",
                column: "EvaluationScaleId",
                principalTable: "EvaluationScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_EvaluationScales_EvaluationScaleId",
                table: "Sections",
                column: "EvaluationScaleId",
                principalTable: "EvaluationScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Forms_FormId",
                table: "Sections",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criteria_Sections_SectionId",
                table: "Criteria");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationScaleOptions_EvaluationScales_EvaluationScaleId",
                table: "EvaluationScaleOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_EvaluationScales_EvaluationScaleId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Forms_FormId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationScales",
                table: "EvaluationScales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationScaleOptions",
                table: "EvaluationScaleOptions");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "Section");

            migrationBuilder.RenameTable(
                name: "EvaluationScales",
                newName: "EvaluationScale");

            migrationBuilder.RenameTable(
                name: "EvaluationScaleOptions",
                newName: "EvaluationScaleOption");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_FormId",
                table: "Section",
                newName: "IX_Section_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_EvaluationScaleId",
                table: "Section",
                newName: "IX_Section_EvaluationScaleId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluationScaleOptions_EvaluationScaleId",
                table: "EvaluationScaleOption",
                newName: "IX_EvaluationScaleOption_EvaluationScaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                table: "Section",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationScale",
                table: "EvaluationScale",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationScaleOption",
                table: "EvaluationScaleOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Criteria_Section_SectionId",
                table: "Criteria",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationScaleOption_EvaluationScale_EvaluationScaleId",
                table: "EvaluationScaleOption",
                column: "EvaluationScaleId",
                principalTable: "EvaluationScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_EvaluationScale_EvaluationScaleId",
                table: "Section",
                column: "EvaluationScaleId",
                principalTable: "EvaluationScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Forms_FormId",
                table: "Section",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
