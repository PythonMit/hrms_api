using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeContractModifyColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractendDate",
                table: "EmployeeContracts",
                newName: "ContractEndDate");

            migrationBuilder.RenameColumn(
                name: "TrainningPeriod",
                table: "EmployeeContracts",
                newName: "TrainingPeriod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractEndDate",
                table: "EmployeeContracts",
                newName: "ContractendDate");

            migrationBuilder.RenameColumn(
                name: "TrainingPeriod",
                table: "EmployeeContracts",
                newName: "TrainningPeriod");
        }
    }
}
