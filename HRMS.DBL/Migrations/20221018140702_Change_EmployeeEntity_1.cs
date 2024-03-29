using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Change_EmployeeEntity_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeStatuses",
                table: "EmployeeStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EmployeeStatuses",
                newName: "EmployeeStatus");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "EmployeeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "EmployeeDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "EmployeeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeStatus",
                table: "EmployeeStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatus_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "EmployeeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatus_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeStatus",
                table: "EmployeeStatus");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeDetails");

            migrationBuilder.RenameTable(
                name: "EmployeeStatus",
                newName: "EmployeeStatuses");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime2",
                unicode: false,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Employees",
                type: "datetime2",
                unicode: false,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeStatuses",
                table: "EmployeeStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "EmployeeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
