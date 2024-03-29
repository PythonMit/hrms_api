using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class InsertEmployeeIncentiveDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeIncentiveStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeIncentiveStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeIncentiveDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeContractId = table.Column<int>(type: "int", nullable: false),
                    IncentiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IncentiveAmount = table.Column<double>(type: "float", nullable: true),
                    EmployeeIncentiveStatusId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeIncentiveDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeIncentiveDetails_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeIncentiveDetails_EmployeeIncentiveStatus_EmployeeIncentiveStatusId",
                        column: x => x.EmployeeIncentiveStatusId,
                        principalTable: "EmployeeIncentiveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeIncentiveDetails_EmployeeContractId",
                table: "EmployeeIncentiveDetails",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeIncentiveDetails_EmployeeIncentiveStatusId",
                table: "EmployeeIncentiveDetails",
                column: "EmployeeIncentiveStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeIncentiveDetails");

            migrationBuilder.DropTable(
                name: "EmployeeIncentiveStatus");
        }
    }
}
