using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeLeaveTransactiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeLeaveTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeContractId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added = table.Column<double>(type: "float", nullable: true),
                    Used = table.Column<double>(type: "float", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveTransaction_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveTransaction_EmployeeContractId",
                table: "EmployeeLeaveTransaction",
                column: "EmployeeContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeaveTransaction");
        }
    }
}
