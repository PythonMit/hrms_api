using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_Location_AddColumn_MetroCity_EmployeeContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_EmployeeContractStatus_EmployeeContractStatusID",
                table: "EmployeeContracts");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractStatusID",
                table: "EmployeeContracts",
                newName: "EmployeeContractStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeContracts_EmployeeContractStatusID",
                table: "EmployeeContracts",
                newName: "IX_EmployeeContracts_EmployeeContractStatusId");

            migrationBuilder.AddColumn<string>(
                name: "MetroCity",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_EmployeeContractStatus_EmployeeContractStatusId",
                table: "EmployeeContracts",
                column: "EmployeeContractStatusId",
                principalTable: "EmployeeContractStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_EmployeeContractStatus_EmployeeContractStatusId",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "MetroCity",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractStatusId",
                table: "EmployeeContracts",
                newName: "EmployeeContractStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeContracts_EmployeeContractStatusId",
                table: "EmployeeContracts",
                newName: "IX_EmployeeContracts_EmployeeContractStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_EmployeeContractStatus_EmployeeContractStatusID",
                table: "EmployeeContracts",
                column: "EmployeeContractStatusID",
                principalTable: "EmployeeContractStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
