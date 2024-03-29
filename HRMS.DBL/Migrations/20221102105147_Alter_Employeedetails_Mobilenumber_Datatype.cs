using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_Employeedetails_Mobilenumber_Datatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "EmployeeDetails",
                type: "nvarchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AlternateMobileNumber",
                table: "EmployeeDetails",
                type: "nvarchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "EmployeeDetails",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlternateMobileNumber",
                table: "EmployeeDetails",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
