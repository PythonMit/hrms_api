using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectTraineeDesignationTypesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "Order", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[] { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Project Trainee", 23, 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 22);
        }
    }
}
