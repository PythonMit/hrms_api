using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterProjectProjectManagerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManager_Employees_EmployeeId",
                table: "ProjectManager");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManager_Project_ProjectId",
                table: "ProjectManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "ProjectManager",
                newName: "ProjectManagers");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectManager_ProjectId",
                table: "ProjectManagers",
                newName: "IX_ProjectManagers_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectManager_EmployeeId",
                table: "ProjectManagers",
                newName: "IX_ProjectManagers_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectManagers",
                table: "ProjectManagers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Employees_EmployeeId",
                table: "ProjectManagers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Projects_ProjectId",
                table: "ProjectManagers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Employees_EmployeeId",
                table: "ProjectManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Projects_ProjectId",
                table: "ProjectManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectManagers",
                table: "ProjectManagers");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "ProjectManagers",
                newName: "ProjectManager");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectManagers_ProjectId",
                table: "ProjectManager",
                newName: "IX_ProjectManager_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectManagers_EmployeeId",
                table: "ProjectManager",
                newName: "IX_ProjectManager_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManager_Employees_EmployeeId",
                table: "ProjectManager",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManager_Project_ProjectId",
                table: "ProjectManager",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
