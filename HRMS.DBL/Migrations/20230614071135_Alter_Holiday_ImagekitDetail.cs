using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterHolidayImagekitDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeHolidays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_ImagekitDetailFileId",
                table: "EmployeeContracts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ImagekitDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImagekitDetailId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImagekitDetailId",
                table: "EmployeeDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImagekitDetailId",
                table: "EmployeeContracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails",
                columns: new[] { "Id", "FileId" });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Holidays");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ImagekitDetails");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailId",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailId",
                table: "EmployeeContracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagekitDetails",
                table: "ImagekitDetails",
                column: "FileId");

            migrationBuilder.CreateTable(
                name: "EmployeeHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHolidays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ImagekitDetailFileId",
                table: "Employees",
                column: "ImagekitDetailFileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailFileId",
                table: "EmployeeDocuments",
                column: "ImagekitDetailFileId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeDocuments",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailFileId",
                table: "Employees",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");
        }
    }
}
