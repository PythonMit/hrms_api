using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_DesignationType_EmployeeContract_EmployeeEarningGross : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "PF",
                table: "EmployeeEarningGross",
                newName: "EmployerPF");

            migrationBuilder.AddColumn<double>(
                name: "EmployeePF",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[] { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trainee", 1, null });

            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[] { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Project Trainee", 1, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DropColumn(
                name: "EmployeePF",
                table: "EmployeeEarningGross");

            migrationBuilder.RenameColumn(
                name: "EmployerPF",
                table: "EmployeeEarningGross",
                newName: "PF");

        }
    }
}
