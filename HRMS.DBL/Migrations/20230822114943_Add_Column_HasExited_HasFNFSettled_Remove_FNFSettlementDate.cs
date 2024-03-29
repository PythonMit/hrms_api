using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnHasExitedHasFNFSettledRemoveFNFSettlementDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullNFinalSatelementBy",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "FullNFinalSatelementDate",
                table: "EmployeeDetails");

            migrationBuilder.AddColumn<bool>(
                name: "HasExited",
                table: "EmployeeDetails",
                type: "bit",
                unicode: false,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFNFSettled",
                table: "EmployeeDetails",
                type: "bit",
                unicode: false,
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasExited",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "HasFNFSettled",
                table: "EmployeeDetails");

            migrationBuilder.AddColumn<string>(
                name: "FullNFinalSatelementBy",
                table: "EmployeeDetails",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FullNFinalSatelementDate",
                table: "EmployeeDetails",
                type: "datetime2",
                unicode: false,
                nullable: true);
        }
    }
}
