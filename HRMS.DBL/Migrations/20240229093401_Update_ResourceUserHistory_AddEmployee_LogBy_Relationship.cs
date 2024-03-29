using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResourceUserHistoryAddEmployeeLogByRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceUserHistory_LogBy",
                table: "ResourceUserHistory",
                column: "LogBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceUserHistory_Employees_LogBy",
                table: "ResourceUserHistory",
                column: "LogBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceUserHistory_Employees_LogBy",
                table: "ResourceUserHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceUserHistory_LogBy",
                table: "ResourceUserHistory");
        }
    }
}
