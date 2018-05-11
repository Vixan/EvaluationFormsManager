using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF.Migrations
{
    public partial class AddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Forms");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Forms",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forms_StatusId",
                table: "Forms",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Status_StatusId",
                table: "Forms",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Status_StatusId",
                table: "Forms");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Forms_StatusId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Forms");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Forms",
                nullable: false,
                defaultValue: false);
        }
    }
}
