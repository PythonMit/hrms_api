using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApprovedByEmployeeLeaveApplicationForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplicationManagers_EmployeeLeaves_EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplications_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplicationManagers_EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers");

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "EmployeeLeaveApplications",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_ApprovedBy",
                table: "EmployeeLeaveApplications",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplicationManagers_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers",
                column: "EmployeeLeaveApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplicationManagers_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers",
                column: "EmployeeLeaveApplicationId",
                principalTable: "EmployeeLeaveApplications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_ApprovedBy",
                table: "EmployeeLeaveApplications",
                column: "ApprovedBy",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplicationManagers_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_ApprovedBy",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplications_ApprovedBy",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplicationManagers_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers");

            migrationBuilder.DropColumn(
                name: "EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "EmployeeLeaveApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeLeaveApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications",
                column: "ImagekitDetailFileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplicationManagers_EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers",
                column: "EmployeeLeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplicationManagers_EmployeeLeaves_EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers",
                column: "EmployeeLeaveId",
                principalTable: "EmployeeLeaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");
        }
    }
}
