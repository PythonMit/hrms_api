using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEmployeeFNFDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFNFDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", unicode: false, nullable: false),
                    FNFDueDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: true),
                    ExitNote = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    HasCertificateIssued = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    HasSalaryProceed = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: true),
                    SettlementBy = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFNFDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFNFDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFNFDetails_EmployeeId",
                table: "EmployeeFNFDetails",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFNFDetails");
        }
    }
}
