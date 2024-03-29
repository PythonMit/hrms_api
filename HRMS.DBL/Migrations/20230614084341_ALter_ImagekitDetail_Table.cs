using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class ALterImagekitDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailId_ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ImagekitDetailId_ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "ImagekitDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagekitDetailFileId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AdjustmentAmount",
                table: "EmployeeEarningGross",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ImagekitDetailId",
                table: "Employees",
                column: "ImagekitDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailId",
                table: "EmployeeDocuments",
                column: "ImagekitDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ImagekitDetailId",
                table: "EmployeeContracts",
                column: "ImagekitDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailId",
                table: "EmployeeContracts",
                column: "ImagekitDetailId",
                principalTable: "ImagekitDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailId",
                table: "EmployeeDocuments",
                column: "ImagekitDetailId",
                principalTable: "ImagekitDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailId",
                table: "Employees",
                column: "ImagekitDetailId",
                principalTable: "ImagekitDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailId",
                table: "EmployeeContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailId",
                table: "EmployeeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ImagekitDetailId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailId",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_ImagekitDetailId",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "AdjustmentAmount",
                table: "EmployeeEarningGross");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "ImagekitDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagekitDetailFileId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeDocuments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeContracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails",
                columns: new[] { "Id", "FileId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ImagekitDetailId_ImagekitDetailFileId",
                table: "Employees",
                columns: new[] { "ImagekitDetailId", "ImagekitDetailFileId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeDocuments",
                columns: new[] { "ImagekitDetailId", "ImagekitDetailFileId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeContracts",
                columns: new[] { "ImagekitDetailId", "ImagekitDetailFileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeContracts",
                columns: new[] { "ImagekitDetailId", "ImagekitDetailFileId" },
                principalTable: "ImagekitDetails",
                principalColumns: new[] { "Id", "FileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailId_ImagekitDetailFileId",
                table: "EmployeeDocuments",
                columns: new[] { "ImagekitDetailId", "ImagekitDetailFileId" },
                principalTable: "ImagekitDetails",
                principalColumns: new[] { "Id", "FileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailId_ImagekitDetailFileId",
                table: "Employees",
                columns: new[] { "ImagekitDetailId", "ImagekitDetailFileId" },
                principalTable: "ImagekitDetails",
                principalColumns: new[] { "Id", "FileId" });
        }
    }
}
