using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeOverTimeEmployeeLeaveApplicationEntityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOverTimes_EmployeeOverTimeStatus_EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId1",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId2",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "DocAttachement",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId1",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId2",
                table: "EmployeeLeaveApplications");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "SystemFlags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OverTimeMinutes",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OverTimeDate",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedMinutes",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "FirstProjectManagerId",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondProjectManagerId",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeaveToDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeaveFromDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "FirstProjectManagerId",
                table: "EmployeeLeaveApplications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeLeaveApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondProjectManagerId",
                table: "EmployeeLeaveApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimes_FirstProjectManagerId",
                table: "EmployeeOverTimes",
                column: "FirstProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimes_SecondProjectManagerId",
                table: "EmployeeOverTimes",
                column: "SecondProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_FirstProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "FirstProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications",
                column: "ImagekitDetailFileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_SecondProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "SecondProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_FirstProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "FirstProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id", 
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_SecondProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "SecondProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_EmployeeOverTimeStatus_EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes",
                column: "EmployeeOverTimeStatusId",
                principalTable: "EmployeeOverTimeStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_Employees_FirstProjectManagerId",
                table: "EmployeeOverTimes",
                column: "FirstProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_Employees_SecondProjectManagerId",
                table: "EmployeeOverTimes",
                column: "SecondProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_FirstProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_SecondProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOverTimes_EmployeeOverTimeStatus_EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOverTimes_Employees_FirstProjectManagerId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOverTimes_Employees_SecondProjectManagerId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeOverTimes_FirstProjectManagerId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeOverTimes_SecondProjectManagerId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplications_FirstProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplications_ImagekitDetailFileId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplications_SecondProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "SystemFlags");

            migrationBuilder.DropColumn(
                name: "FirstProjectManagerId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "SecondProjectManagerId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "FirstProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "SecondProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OverTimeMinutes",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OverTimeDate",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedMinutes",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerId1",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerId2",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeaveToDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeaveFromDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocAttachement",
                table: "EmployeeLeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "EmployeeLeaveApplications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerId1",
                table: "EmployeeLeaveApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerId2",
                table: "EmployeeLeaveApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_EmployeeOverTimeStatus_EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes",
                column: "EmployeeOverTimeStatusId",
                principalTable: "EmployeeOverTimeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
