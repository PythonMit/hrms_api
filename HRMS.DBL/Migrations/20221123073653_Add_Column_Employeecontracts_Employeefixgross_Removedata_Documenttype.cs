using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Add_Column_Employeecontracts_Employeefixgross_Removedata_Documenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextIncentiveDate",
                table: "EmployeeFixGross",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractDocument",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextIncentiveDate",
                table: "EmployeeFixGross");

            migrationBuilder.DropColumn(
                name: "ContractDocument",
                table: "EmployeeContracts");

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Contract", 1, null });
        }
    }
}
