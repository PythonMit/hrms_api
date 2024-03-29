using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class EmployeesDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EmployeeCode = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Branch = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    Gender = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ProfilePhoto = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    EmployeeStatusesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeStatus_EmployeeStatusesId",
                        column: x => x.EmployeeStatusesId,
                        principalTable: "EmployeeStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    WorkEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PersonalEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MobileNumber = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false),
                    AlternateMobileNumber = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false),
                    PresentAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PermanentAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PreviousEmployeer = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Experience = table.Column<int>(type: "int", unicode: false, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    FullNFinalSatelementDate = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    FullNFinalSatelementBy = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AllowEditPersonalDetails = table.Column<bool>(type: "bit", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 1, "Active" });

            migrationBuilder.InsertData(
                table: "EmployeeStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 2, "InActive" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeID",
                table: "EmployeeDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeStatusesId",
                table: "Employees",
                column: "EmployeeStatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeStatus");
        }
    }
}
