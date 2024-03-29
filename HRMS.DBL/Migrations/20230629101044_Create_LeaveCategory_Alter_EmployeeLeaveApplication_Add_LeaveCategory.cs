using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class CreateLeaveCategoryAlterEmployeeLeaveApplicationAddLeaveCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveCategoryId",
                table: "EmployeeLeaveApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LeaveCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LeaveCategories",
                columns: new[] { "Id", "Category", "CreatedDateTimeUtc", "Description", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[,]
                {
                    { 1, "Casual Leave", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 2, "Privilege Leave", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 3, "Sick Leave", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_LeaveCategoryId",
                table: "EmployeeLeaveApplications",
                column: "LeaveCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_LeaveCategories_LeaveCategoryId",
                table: "EmployeeLeaveApplications",
                column: "LeaveCategoryId",
                principalTable: "LeaveCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_LeaveCategories_LeaveCategoryId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropTable(
                name: "LeaveCategories");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplications_LeaveCategoryId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "LeaveCategoryId",
                table: "EmployeeLeaveApplications");
        }
    }
}
