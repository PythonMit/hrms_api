using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeLeaveApplicationCommentCommentBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeLeaveApplicationManagers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CommentBy",
                table: "EmployeeLeaveApplicationComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplicationComments_CommentBy",
                table: "EmployeeLeaveApplicationComments",
                column: "CommentBy");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveApplicationComments_Employees_CommentBy",
                table: "EmployeeLeaveApplicationComments",
                column: "CommentBy",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveApplicationComments_Employees_CommentBy",
                table: "EmployeeLeaveApplicationComments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaveApplicationComments_CommentBy",
                table: "EmployeeLeaveApplicationComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationManagers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeLeaveApplicationManagers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommentBy",
                table: "EmployeeLeaveApplicationComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
