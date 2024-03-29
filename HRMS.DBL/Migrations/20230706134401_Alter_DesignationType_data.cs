using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterDesignationTypedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Sr. Software Developer");

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 25,
                column: "Name",
                value: "Software Developer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Project Trainee");

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 25,
                column: "Name",
                value: "Project Trainee");
        }
    }
}
