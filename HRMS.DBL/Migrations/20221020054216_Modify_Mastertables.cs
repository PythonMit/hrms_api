using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Modify_Mastertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeeOverTimeStatus");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeeEarningGrossStatus");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeeContractStatus");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DesignationTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "DesignationName",
                table: "DesignationTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BranchName",
                table: "Branches",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BranchCode",
                table: "Branches",
                newName: "Code");

            migrationBuilder.AlterColumn<int>(
                name: "TotalLeaves",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "LeaveTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "LeaveTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "EmployeeOverTimeStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "EmployeeOverTimeStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeOverTimeStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "EmployeeEarningGrossStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "EmployeeEarningGrossStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeEarningGrossStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "EmployeeContractStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "EmployeeContractStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeContractStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "DocumentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "DesignationTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "DesignationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "DesignationTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "Branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "Branches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalLeaves",
                value: 5);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalLeaves",
                value: 8);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalLeaves",
                value: 5);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "TotalLeaves",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "EmployeeOverTimeStatus");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "EmployeeOverTimeStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeOverTimeStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "EmployeeEarningGrossStatus");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "EmployeeEarningGrossStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeEarningGrossStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "EmployeeContractStatus");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "EmployeeContractStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeContractStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "DesignationTypes");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "DesignationTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "DesignationTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DesignationTypes",
                newName: "DesignationName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Branches",
                newName: "BranchName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Branches",
                newName: "BranchCode");

            migrationBuilder.AlterColumn<string>(
                name: "TotalLeaves",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LeaveTypes",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EmployeeOverTimeStatus",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EmployeeEarningGrossStatus",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EmployeeContractStatus",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DocumentTypes",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DesignationTypes",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Branches",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalLeaves",
                value: "5");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalLeaves",
                value: "8");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalLeaves",
                value: "5");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "TotalLeaves",
                value: "3");
        }
    }
}
