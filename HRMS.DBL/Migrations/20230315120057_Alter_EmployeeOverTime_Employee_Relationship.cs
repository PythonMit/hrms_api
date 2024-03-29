using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeOverTimeEmployeeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "OverTimeAmount",
                table: "EmployeeOverTimes",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OverTimeAmount",
                table: "EmployeeOverTimes",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
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
    }
}
