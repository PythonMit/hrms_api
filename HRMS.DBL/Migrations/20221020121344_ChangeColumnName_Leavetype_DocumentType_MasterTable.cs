using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class ChangeColumnName_Leavetype_DocumentType_MasterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveName",
                table: "LeaveTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DocumentName",
                table: "DocumentTypes",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LeaveTypes",
                newName: "LeaveName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DocumentTypes",
                newName: "DocumentName");
        }
    }
}
