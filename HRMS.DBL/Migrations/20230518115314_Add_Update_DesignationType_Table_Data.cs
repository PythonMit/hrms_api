using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateDesignationTypeTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "DesignationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Order",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Order",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Order",
                value: 6);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Order",
                value: 7);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Order",
                value: 8);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Sr. FrontEnd Developer", 9 });

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Order",
                value: 10);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "Order",
                value: 11);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "Order",
                value: 12);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Sr. FullStack Developer", 13 });

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "Order",
                value: 14);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "Order",
                value: 15);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "Order",
                value: 16);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "Order",
                value: 17);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "Order",
                value: 18);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "Order",
                value: 19);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "Order",
                value: 20);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "Order",
                value: 21);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 21,
                column: "Order",
                value: 22);

            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "Order", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[] { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chief Technology Officer", 3, 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DropColumn(
                name: "Order",
                table: "DesignationTypes");

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Sr.FrontEnd Developer");

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Sr.FullStack Developer");

            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[] { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Project Trainee", 1, null });
        }
    }
}
