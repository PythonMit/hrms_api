using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagekitDetailFileIdEmployeeContractImagekitDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDocument",
                table: "EmployeeContracts");

            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeContracts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ImagekitDetailFileId",
                table: "EmployeeContracts",
                column: "ImagekitDetailFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeContracts",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.AddColumn<string>(
                name: "ContractDocument",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
