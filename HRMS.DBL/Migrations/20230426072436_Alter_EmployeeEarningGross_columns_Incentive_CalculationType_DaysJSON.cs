using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeEarningGrosscolumnsIncentiveCalculationTypeDaysJSON : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalculationType",
                table: "EmployeeEarningGross",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DaysJSON",
                table: "EmployeeEarningGross",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Incentive",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationType",
                table: "EmployeeEarningGross");

            migrationBuilder.DropColumn(
                name: "DaysJSON",
                table: "EmployeeEarningGross");

            migrationBuilder.DropColumn(
                name: "Incentive",
                table: "EmployeeEarningGross");
        }
    }
}
