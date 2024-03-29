using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class ChangeColumnName_DesignationTypeID_DesignationTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_Employees_DesignationTypes_DesignationTypeID",
            table: "Employees");

            migrationBuilder.RenameColumn(
                name: "DesignationTypeID",
                table: "Employees",
                newName: "DesignationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DesignationTypeID",
                table: "Employees",
                newName: "IX_Employees_DesignationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_DesignationTypes_DesignationTypeId",
                table: "Employees",
                column: "DesignationTypeId",
                principalTable: "DesignationTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_DesignationTypes_DesignationTypeId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "DesignationTypeId",
                table: "Employees",
                newName: "DesignationTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DesignationTypeId",
                table: "Employees",
                newName: "IX_Employees_DesignationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_DesignationTypes_DesignationTypeID",
                table: "Employees",
                column: "DesignationTypeID",
                principalTable: "DesignationTypes",
                principalColumn: "Id");
        }
    }
}
