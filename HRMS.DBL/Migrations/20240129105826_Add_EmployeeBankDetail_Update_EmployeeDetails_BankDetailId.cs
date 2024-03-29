using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeBankDetailUpdateEmployeeDetailsBankDetailId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankDetailId",
                table: "EmployeeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeBankDetailId",
                table: "EmployeeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeBankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryACNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBankDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeBankDetailId",
                table: "EmployeeDetails",
                column: "EmployeeBankDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeBankDetails_EmployeeBankDetailId",
                table: "EmployeeDetails",
                column: "EmployeeBankDetailId",
                principalTable: "EmployeeBankDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeBankDetails_EmployeeBankDetailId",
                table: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "EmployeeBankDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmployeeBankDetailId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "BankDetailId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeBankDetailId",
                table: "EmployeeDetails");
        }
    }
}
