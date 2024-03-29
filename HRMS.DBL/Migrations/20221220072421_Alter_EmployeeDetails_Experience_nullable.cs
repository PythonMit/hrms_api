using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_EmployeeDetails_Experience_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Experience",
                table: "EmployeeDetails",
                type: "int",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Experience",
                table: "EmployeeDetails",
                type: "int",
                unicode: false,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
