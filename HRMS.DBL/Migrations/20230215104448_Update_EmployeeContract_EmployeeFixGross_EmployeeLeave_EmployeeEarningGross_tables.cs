using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeContractEmployeeFixGrossEmployeeLeaveEmployeeEarningGrosstables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_DesignationTypes_DesignationTypeID",
                table: "EmployeeContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEarningGross_EmployeeContracts_EmployeeContractID",
                table: "EmployeeEarningGross");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFixGross_EmployeeContracts_EmployeeContractID",
                table: "EmployeeFixGross");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFixGross_EmployeeContractID",
                table: "EmployeeFixGross");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractID",
                table: "EmployeeFixGross",
                newName: "EmployeeContractId");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractID",
                table: "EmployeeEarningGross",
                newName: "EmployeeContractId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEarningGross_EmployeeContractID",
                table: "EmployeeEarningGross",
                newName: "IX_EmployeeEarningGross_EmployeeContractId");

            migrationBuilder.RenameColumn(
                name: "DesignationTypeID",
                table: "EmployeeContracts",
                newName: "DesignationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeContracts_DesignationTypeID",
                table: "EmployeeContracts",
                newName: "IX_EmployeeContracts_DesignationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFixGross_EmployeeContractId",
                table: "EmployeeFixGross",
                column: "EmployeeContractId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_DesignationTypes_DesignationTypeId",
                table: "EmployeeContracts",
                column: "DesignationTypeId",
                principalTable: "DesignationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEarningGross_EmployeeContracts_EmployeeContractId",
                table: "EmployeeEarningGross",
                column: "EmployeeContractId",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFixGross_EmployeeContracts_EmployeeContractId",
                table: "EmployeeFixGross",
                column: "EmployeeContractId",
                principalTable: "EmployeeContracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_DesignationTypes_DesignationTypeId",
                table: "EmployeeContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEarningGross_EmployeeContracts_EmployeeContractId",
                table: "EmployeeEarningGross");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFixGross_EmployeeContracts_EmployeeContractId",
                table: "EmployeeFixGross");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFixGross_EmployeeContractId",
                table: "EmployeeFixGross");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractId",
                table: "EmployeeFixGross",
                newName: "EmployeeContractID");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractId",
                table: "EmployeeEarningGross",
                newName: "EmployeeContractID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEarningGross_EmployeeContractId",
                table: "EmployeeEarningGross",
                newName: "IX_EmployeeEarningGross_EmployeeContractID");

            migrationBuilder.RenameColumn(
                name: "DesignationTypeId",
                table: "EmployeeContracts",
                newName: "DesignationTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeContracts_DesignationTypeId",
                table: "EmployeeContracts",
                newName: "IX_EmployeeContracts_DesignationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFixGross_EmployeeContractID",
                table: "EmployeeFixGross",
                column: "EmployeeContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_DesignationTypes_DesignationTypeID",
                table: "EmployeeContracts",
                column: "DesignationTypeID",
                principalTable: "DesignationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEarningGross_EmployeeContracts_EmployeeContractID",
                table: "EmployeeEarningGross",
                column: "EmployeeContractID",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFixGross_EmployeeContracts_EmployeeContractID",
                table: "EmployeeFixGross",
                column: "EmployeeContractID",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
