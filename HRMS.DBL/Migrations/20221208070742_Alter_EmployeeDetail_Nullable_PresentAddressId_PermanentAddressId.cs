using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_EmployeeDetail_Nullable_PresentAddressId_PermanentAddressId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PermanentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PresentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.AlterColumn<int>(
                name: "PresentAddressId",
                table: "EmployeeDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PermanentAddressId",
                table: "EmployeeDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PermanentAddressId",
                table: "EmployeeDetails",
                column: "PermanentAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PresentAddressId",
                table: "EmployeeDetails",
                column: "PresentAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PermanentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PresentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.AlterColumn<int>(
                name: "PresentAddressId",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PermanentAddressId",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PermanentAddressId",
                table: "EmployeeDetails",
                column: "PermanentAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PresentAddressId",
                table: "EmployeeDetails",
                column: "PresentAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
