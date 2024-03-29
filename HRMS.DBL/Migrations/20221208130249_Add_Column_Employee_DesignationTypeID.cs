using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Add_Column_Employee_DesignationTypeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesignationTypeID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationTypeID",
                table: "Employees",
                column: "DesignationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_DesignationTypes_DesignationTypeID",
                table: "Employees",
                column: "DesignationTypeID",
                principalTable: "DesignationTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_DesignationTypes_DesignationTypeID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DesignationTypeID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DesignationTypeID",
                table: "Employees");
        }
    }
}
