using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnOvertimeAmountReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByEmployeeId",
                table: "EmployeeOverTimes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimes_ApprovedByEmployeeId",
                table: "EmployeeOverTimes",
                column: "ApprovedByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_Employees_ApprovedByEmployeeId",
                table: "EmployeeOverTimes",
                column: "ApprovedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOverTimes_Employees_ApprovedByEmployeeId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeOverTimes_ApprovedByEmployeeId",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "ApprovedByEmployeeId",
                table: "EmployeeOverTimes");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "EmployeeOverTimes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
