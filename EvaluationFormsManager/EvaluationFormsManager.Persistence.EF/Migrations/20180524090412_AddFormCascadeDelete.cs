using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationFormsManager.Persistence.EF.Migrations
{
    public partial class AddFormCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criteria_Sections_SectionId",
                table: "Criteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Forms_FormId",
                table: "Sections");

            migrationBuilder.AddForeignKey(
                name: "FK_Criteria_Sections_SectionId",
                table: "Criteria",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Forms_FormId",
                table: "Sections",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criteria_Sections_SectionId",
                table: "Criteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Forms_FormId",
                table: "Sections");

            migrationBuilder.AddForeignKey(
                name: "FK_Criteria_Sections_SectionId",
                table: "Criteria",
                column: "SectionId",
                principalTable: "Sections",
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
    }
}
