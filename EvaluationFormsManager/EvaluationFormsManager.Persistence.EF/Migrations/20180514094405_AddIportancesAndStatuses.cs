using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF.Migrations
{
    public partial class AddIportancesAndStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Importance_ImportanceId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Status_StatusId",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Importance",
                table: "Importance");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Importance",
                newName: "Importances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Importances",
                table: "Importances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Importances_ImportanceId",
                table: "Forms",
                column: "ImportanceId",
                principalTable: "Importances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Statuses_StatusId",
                table: "Forms",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Importances_ImportanceId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Statuses_StatusId",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Importances",
                table: "Importances");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Importances",
                newName: "Importance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Importance",
                table: "Importance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Importance_ImportanceId",
                table: "Forms",
                column: "ImportanceId",
                principalTable: "Importance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Status_StatusId",
                table: "Forms",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
