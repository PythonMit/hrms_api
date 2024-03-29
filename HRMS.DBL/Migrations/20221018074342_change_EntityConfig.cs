using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class change_EntityConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatus_Status",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeStatus",
                table: "EmployeeStatus");

            migrationBuilder.RenameTable(
                name: "EmployeeStatus",
                newName: "EmployeeStatuses");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Employees",
                newName: "EmployeeStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Status",
                table: "Employees",
                newName: "IX_Employees_EmployeeStatusId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FullNFinalSatelementDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeStatuses",
                table: "EmployeeStatuses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "EmployeeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeStatuses",
                table: "EmployeeStatuses");

            migrationBuilder.RenameTable(
                name: "EmployeeStatuses",
                newName: "EmployeeStatus");

            migrationBuilder.RenameColumn(
                name: "EmployeeStatusId",
                table: "Employees",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeStatusId",
                table: "Employees",
                newName: "IX_Employees_Status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FullNFinalSatelementDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeStatus",
                table: "EmployeeStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatus_Status",
                table: "Employees",
                column: "Status",
                principalTable: "EmployeeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
