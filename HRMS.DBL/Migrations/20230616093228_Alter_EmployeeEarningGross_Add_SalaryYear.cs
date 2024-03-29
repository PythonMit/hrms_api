using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeEarningGrossAddSalaryYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryYear",
                table: "EmployeeEarningGross",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryYear",
                table: "EmployeeEarningGross");
        }
    }
}
