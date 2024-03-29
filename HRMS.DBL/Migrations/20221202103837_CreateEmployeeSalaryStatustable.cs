using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class CreateEmployeeSalaryStatustable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeSalaryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmployeeSalaryStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "InProcess", null });

            migrationBuilder.InsertData(
                table: "EmployeeSalaryStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Hold", null });

            migrationBuilder.InsertData(
                table: "EmployeeSalaryStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Paid", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSalaryStatus");
        }
    }
}
