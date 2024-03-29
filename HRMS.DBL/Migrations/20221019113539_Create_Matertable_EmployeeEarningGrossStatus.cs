using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Create_Matertable_EmployeeEarningGrossStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeEarningGrossStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEarningGrossStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmployeeEarningGrossStatus",
                columns: new[] { "Id", "Description", "StatusType" },
                values: new object[] { 1, null, "InProcess" });

            migrationBuilder.InsertData(
                table: "EmployeeEarningGrossStatus",
                columns: new[] { "Id", "Description", "StatusType" },
                values: new object[] { 2, null, "Hold" });

            migrationBuilder.InsertData(
                table: "EmployeeEarningGrossStatus",
                columns: new[] { "Id", "Description", "StatusType" },
                values: new object[] { 3, null, "Paid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEarningGrossStatus");
        }
    }
}
