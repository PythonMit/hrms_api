using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Create_Mastertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContractStatus",
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
                    table.PrimaryKey("PK_EmployeeContractStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOverTimeStatus",
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
                    table.PrimaryKey("PK_EmployeeOverTimeStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalLeaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "BranchCode", "BranchName", "Description" },
                values: new object[,]
                {
                    { 1, "ST", "Surat", null },
                    { 2, "AHM", "Ahmedabad", null }
                });

            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "Description", "DesignationName" },
                values: new object[,]
                {
                    { 1, null, "Director" },
                    { 2, null, "HRExecutive" },
                    { 3, null, "Project Manager" },
                    { 4, null, "Team Leader" },
                    { 5, null, "BDM" },
                    { 6, null, "BDE" },
                    { 7, null, "FrontEnd Developer" },
                    { 8, null, "Sr.FrontEnd Developer" },
                    { 9, null, "BackEnd Developer" },
                    { 10, null, "Sr. BackEnd Developer" },
                    { 11, null, "FullStack Developer" },
                    { 12, null, "Sr.FullStack Developer" },
                    { 13, null, "Mobile Developer" },
                    { 14, null, "Sr.Mobile Developer" },
                    { 15, null, "UI / UX Designer" },
                    { 16, null, "Web Designer" },
                    { 17, null, "Sr. Web Designer" },
                    { 18, null, "Software Engineer" },
                    { 19, null, "PHP Developer" },
                    { 20, null, "Sr. PHP Developer" }
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "Description", "DocumentName" },
                values: new object[,]
                {
                    { 1, null, "PAN" },
                    { 2, null, "AadhaarCard" },
                    { 3, null, "ElectionCard" },
                    { 4, null, "Contract" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeContractStatus",
                columns: new[] { "Id", "Description", "StatusType" },
                values: new object[,]
                {
                    { 1, null, "Running" },
                    { 2, null, "Drop" },
                    { 3, null, "NoticePeriod" },
                    { 4, null, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeOverTimeStatus",
                columns: new[] { "Id", "Description", "StatusType" },
                values: new object[,]
                {
                    { 1, null, "Pending" },
                    { 2, null, "Approved" },
                    { 3, null, "Declined" }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "Description", "LeaveName", "TotalLeaves" },
                values: new object[,]
                {
                    { 1, null, "Casual Leave", "5" },
                    { 2, null, "Privilege Leave", "8" },
                    { 3, null, "Sick Leave", "5" },
                    { 4, null, "Leave Without Pay", "3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "DesignationTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "EmployeeContractStatus");

            migrationBuilder.DropTable(
                name: "EmployeeOverTimeStatus");

            migrationBuilder.DropTable(
                name: "LeaveTypes");
        }
    }
}
