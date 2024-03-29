using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificationAddEmployeeIdUpdateSalaryStatusAddPartialPaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeSalaryStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusType",
                value: "In Process");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_EmployeeId",
                table: "Notification",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Employees_EmployeeId",
                table: "Notification",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Employees_EmployeeId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_EmployeeId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "EmployeeSalaryStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusType",
                value: "InProcess");
        }
    }
}
