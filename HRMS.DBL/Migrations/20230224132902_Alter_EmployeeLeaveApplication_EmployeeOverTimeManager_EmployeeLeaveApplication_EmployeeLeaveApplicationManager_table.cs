using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeLeaveApplicationEmployeeOverTimeManagerEmployeeLeaveApplicationEmployeeLeaveApplicationManagertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplicationComments_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationComments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_FirstProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_SecondProjectManagerId",
                table: "EmployeeLeaveApplications");

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
                name: "IX_EmployeeLeaveApplications_SecondProjectManagerId",
                table: "EmployeeLeaveApplications");

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
                name: "SecondProjectManagerId",
                table: "EmployeeLeaveApplications");

            migrationBuilder.AddColumn<double>(
                name: "OverTimeAmount",
                table: "EmployeeOverTimes",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveApplicationManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeLeaveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveApplicationManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveApplicationManagers_EmployeeLeaves_EmployeeLeaveId",
                        column: x => x.EmployeeLeaveId,
                        principalTable: "EmployeeLeaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveApplicationManagers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOverTimeManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeOvertimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOverTimeManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeOverTimeManagers_EmployeeOverTimes_EmployeeOvertimeId",
                        column: x => x.EmployeeOvertimeId,
                        principalTable: "EmployeeOverTimes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeOverTimeManagers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplicationManagers_EmployeeId",
                table: "EmployeeLeaveApplicationManagers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplicationManagers_EmployeeLeaveId",
                table: "EmployeeLeaveApplicationManagers",
                column: "EmployeeLeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimeManagers_EmployeeId",
                table: "EmployeeOverTimeManagers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimeManagers_EmployeeOvertimeId",
                table: "EmployeeOverTimeManagers",
                column: "EmployeeOvertimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplicationComments_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationComments",
                column: "EmployeeLeaveApplicationId",
                principalTable: "EmployeeLeaveApplications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplicationComments_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationComments");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveApplicationManagers");

            migrationBuilder.DropTable(
                name: "EmployeeOverTimeManagers");

            migrationBuilder.DropColumn(
                name: "OverTimeAmount",
                table: "EmployeeOverTimes");

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

            migrationBuilder.AddColumn<int>(
                name: "FirstProjectManagerId",
                table: "EmployeeLeaveApplications",
                type: "int",
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
                name: "IX_EmployeeLeaveApplications_SecondProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "SecondProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplicationComments_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationComments",
                column: "EmployeeLeaveApplicationId",
                principalTable: "EmployeeLeaveApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_FirstProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "FirstProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplications_Employees_SecondProjectManagerId",
                table: "EmployeeLeaveApplications",
                column: "SecondProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_Employees_FirstProjectManagerId",
                table: "EmployeeOverTimes",
                column: "FirstProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_Employees_SecondProjectManagerId",
                table: "EmployeeOverTimes",
                column: "SecondProjectManagerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
