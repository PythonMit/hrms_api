using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class CreateEmployee_Flowrelated_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatus_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PermanentAddress",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "PresentAddress",
                table: "EmployeeDetails");

            migrationBuilder.RenameColumn(
                name: "EmployeeStatusId",
                table: "Employees",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeStatusId",
                table: "Employees",
                newName: "IX_Employees_BranchId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhoto",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermanentAddressId",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PresentAddressId",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<int>(type: "int", nullable: false),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProbationPeriod = table.Column<int>(type: "int", nullable: false),
                    TrainningPeriod = table.Column<int>(type: "int", nullable: false),
                    DesignationTypeID = table.Column<int>(type: "int", nullable: false),
                    EmployeeContractStatusID = table.Column<int>(type: "int", nullable: false),
                    DropDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DropReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticePeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoticePeriodEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_DesignationTypes_DesignationTypeID",
                        column: x => x.DesignationTypeID,
                        principalTable: "DesignationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_EmployeeContractStatus_EmployeeContractStatusID",
                        column: x => x.EmployeeContractStatusID,
                        principalTable: "EmployeeContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveStatus",
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
                    table.PrimaryKey("PK_EmployeeLeaveStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEarningGross",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeContractID = table.Column<int>(type: "int", nullable: false),
                    SalaryMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDays = table.Column<double>(type: "float", nullable: false),
                    LWP = table.Column<double>(type: "float", nullable: false),
                    PaidDays = table.Column<double>(type: "float", nullable: false),
                    Basic = table.Column<double>(type: "float", nullable: false),
                    DA = table.Column<double>(type: "float", nullable: false),
                    HRA = table.Column<double>(type: "float", nullable: false),
                    ConveyanceAllowance = table.Column<double>(type: "float", nullable: false),
                    OtherAllowance = table.Column<double>(type: "float", nullable: false),
                    Incentive = table.Column<double>(type: "float", nullable: false),
                    PT = table.Column<int>(type: "int", nullable: false),
                    TDS = table.Column<double>(type: "float", nullable: false),
                    PF = table.Column<double>(type: "float", nullable: false),
                    OverTimeAmount = table.Column<double>(type: "float", nullable: false),
                    NetSalary = table.Column<double>(type: "float", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeEarningGrossStatusId = table.Column<int>(type: "int", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEarningGross", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEarningGross_EmployeeContracts_EmployeeContractID",
                        column: x => x.EmployeeContractID,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEarningGross_EmployeeEarningGrossStatus_EmployeeEarningGrossStatusId",
                        column: x => x.EmployeeEarningGrossStatusId,
                        principalTable: "EmployeeEarningGrossStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeFixGross",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeContractID = table.Column<int>(type: "int", nullable: false),
                    CostToCompany = table.Column<int>(type: "int", nullable: false),
                    Basic = table.Column<double>(type: "float", nullable: false),
                    DA = table.Column<double>(type: "float", nullable: false),
                    HRA = table.Column<double>(type: "float", nullable: false),
                    ConveyanceAllowance = table.Column<int>(type: "int", nullable: false),
                    OtherAllowance = table.Column<int>(type: "int", nullable: false),
                    IsIncentive = table.Column<bool>(type: "bit", nullable: false),
                    IncentiveDuration = table.Column<int>(type: "int", nullable: false),
                    IncentiveRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFixGross", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFixGross_EmployeeContracts_EmployeeContractID",
                        column: x => x.EmployeeContractID,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeContractID = table.Column<int>(type: "int", nullable: false),
                    LeaveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false),
                    TotalLeaves = table.Column<double>(type: "float", nullable: false),
                    TotalLeavesTaken = table.Column<double>(type: "float", nullable: false),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_EmployeeContracts_EmployeeContractID",
                        column: x => x.EmployeeContractID,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_LeaveTypes_LeaveTypeID",
                        column: x => x.LeaveTypeID,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOverTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeContractId = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverTimeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OverTimeMinutes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectManagerId1 = table.Column<int>(type: "int", nullable: false),
                    ProjectManagerId2 = table.Column<int>(type: "int", nullable: false),
                    EmployeeOverTimeStatusId = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedMinutes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOverTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeOverTimes_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOverTimes_EmployeeOverTimeStatus_EmployeeOverTimeStatusId",
                        column: x => x.EmployeeOverTimeStatusId,
                        principalTable: "EmployeeOverTimeStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeContractId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    ApplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfDays = table.Column<double>(type: "float", nullable: false),
                    PurposeOfLeave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeLeaveStatusId = table.Column<int>(type: "int", nullable: false),
                    DocAttachement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectManagerId1 = table.Column<int>(type: "int", nullable: false),
                    ProjectManagerId2 = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveApplications_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveApplications_EmployeeLeaveStatus_EmployeeLeaveStatusId",
                        column: x => x.EmployeeLeaveStatusId,
                        principalTable: "EmployeeLeaveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveApplications_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveApplicationComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeLeaveApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveApplicationComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveApplicationComments_EmployeeLeaveApplications_EmployeeLeaveApplicationId",
                        column: x => x.EmployeeLeaveApplicationId,
                        principalTable: "EmployeeLeaveApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeLeaveStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Pending", null });

            migrationBuilder.InsertData(
                table: "EmployeeLeaveStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Approved", null });

            migrationBuilder.InsertData(
                table: "EmployeeLeaveStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Declined", null });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_PermanentAddressId",
                table: "EmployeeDetails",
                column: "PermanentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_PresentAddressId",
                table: "EmployeeDetails",
                column: "PresentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_DesignationTypeID",
                table: "EmployeeContracts",
                column: "DesignationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeContractStatusID",
                table: "EmployeeContracts",
                column: "EmployeeContractStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_DocumentTypeId",
                table: "EmployeeDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                table: "EmployeeDocuments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEarningGross_EmployeeContractID",
                table: "EmployeeEarningGross",
                column: "EmployeeContractID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEarningGross_EmployeeEarningGrossStatusId",
                table: "EmployeeEarningGross",
                column: "EmployeeEarningGrossStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFixGross_EmployeeContractID",
                table: "EmployeeFixGross",
                column: "EmployeeContractID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplicationComments_EmployeeLeaveApplicationId",
                table: "EmployeeLeaveApplicationComments",
                column: "EmployeeLeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_EmployeeContractId",
                table: "EmployeeLeaveApplications",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_EmployeeLeaveStatusId",
                table: "EmployeeLeaveApplications",
                column: "EmployeeLeaveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveApplications_LeaveTypeId",
                table: "EmployeeLeaveApplications",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_EmployeeContractID",
                table: "EmployeeLeaves",
                column: "EmployeeContractID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_LeaveTypeID",
                table: "EmployeeLeaves",
                column: "LeaveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimes_EmployeeContractId",
                table: "EmployeeOverTimes",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimes_EmployeeOverTimeStatusId",
                table: "EmployeeOverTimes",
                column: "EmployeeOverTimeStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PermanentAddressId",
                table: "EmployeeDetails",
                column: "PermanentAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PresentAddressId",
                table: "EmployeeDetails",
                column: "PresentAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "Id");
               

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PermanentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_EmployeeAddresses_PresentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeAddresses");

            migrationBuilder.DropTable(
                name: "EmployeeDocuments");

            migrationBuilder.DropTable(
                name: "EmployeeEarningGross");

            migrationBuilder.DropTable(
                name: "EmployeeFixGross");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveApplicationComments");

            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "EmployeeOverTimes");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveApplications");

            migrationBuilder.DropTable(
                name: "EmployeeContracts");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveStatus");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_PermanentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_PresentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "PermanentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "PresentAddressId",
                table: "EmployeeDetails");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Employees",
                newName: "EmployeeStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                newName: "IX_Employees_EmployeeStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhoto",
                table: "Employees",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "Employees",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Employees",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentAddress",
                table: "EmployeeDetails",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentAddress",
                table: "EmployeeDetails",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatus_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "EmployeeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
