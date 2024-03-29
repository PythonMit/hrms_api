using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeEarningGrossEmployeeEarningGrossAddLTAMedicalAllowanceChildEducationESI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChildEducation",
                table: "EmployeeFixGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LTA",
                table: "EmployeeFixGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MedicalAllowance",
                table: "EmployeeFixGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ChildEducation",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ESI",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LTA",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MedicalAllowance",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildEducation",
                table: "EmployeeFixGross");

            migrationBuilder.DropColumn(
                name: "LTA",
                table: "EmployeeFixGross");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                table: "EmployeeFixGross");

            migrationBuilder.DropColumn(
                name: "ChildEducation",
                table: "EmployeeEarningGross");

            migrationBuilder.DropColumn(
                name: "ESI",
                table: "EmployeeEarningGross");

            migrationBuilder.DropColumn(
                name: "LTA",
                table: "EmployeeEarningGross");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                table: "EmployeeEarningGross");
        }
    }
}
