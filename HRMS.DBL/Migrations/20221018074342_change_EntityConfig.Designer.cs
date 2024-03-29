﻿// <auto-generated />
using System;
using HRMS.DBL.DbContextConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRMS.DBL.Migrations
{
    [DbContext(typeof(HRMSDbContext))]
    [Migration("20221018074342_change_EntityConfig")]
    partial class change_EntityConfig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HRMS.DBL.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Branch")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Branch");

                    b.Property<DateTime>("CreatedDate")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime>("DateOfBirth")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .HasColumnName("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Email");

                    b.Property<string>("EmployeeCode")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("EmployeeCode");

                    b.Property<int>("EmployeeStatusId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("FirstName");

                    b.Property<string>("Gender")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Gender");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("IsActive");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("LastName");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("MiddleName");

                    b.Property<DateTime>("ModifiedDate")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedDate");

                    b.Property<string>("ProfilePhoto")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("ProfilePhoto");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeStatusId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("HRMS.DBL.Entities.EmployeeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AllowEditPersonalDetails")
                        .IsUnicode(false)
                        .HasColumnType("bit")
                        .HasColumnName("AllowEditPersonalDetails");

                    b.Property<int>("AlternateMobileNumber")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("AlternateMobileNumber");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<int>("Experience")
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Experience");

                    b.Property<string>("FullNFinalSatelementBy")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("FullNFinalSatelementBy");

                    b.Property<DateTime?>("FullNFinalSatelementDate")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .HasColumnName("FullNFinalSatelementDate");

                    b.Property<DateTime?>("JoinDate")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .HasColumnName("JoinDate");

                    b.Property<int>("MobileNumber")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("MobileNumber");

                    b.Property<string>("PermanentAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("PermanentAddress");

                    b.Property<string>("PersonalEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("PersonalEmail");

                    b.Property<string>("PresentAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("PresentAddress");

                    b.Property<string>("PreviousEmployeer")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("PreviousEmployeer");

                    b.Property<string>("WorkEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("WorkEmail");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EmployeeDetails", (string)null);
                });

            modelBuilder.Entity("HRMS.DBL.Entities.EmployeeStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("EmployeeStatuses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Status = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Status = "InActive"
                        });
                });

            modelBuilder.Entity("HRMS.DBL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "HRManager"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Employee"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ProjectManager"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Guest"
                        });
                });

            modelBuilder.Entity("HRMS.DBL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Disabled")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("bit")
                        .HasColumnName("Disabled");

                    b.Property<string>("Emailaddress")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("EmailAddress");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Password")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Password");

                    b.Property<int>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .HasMaxLength(32)
                        .HasColumnType("varbinary(32)");

                    b.Property<string>("Username")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("HRMS.DBL.Entities.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("LastName");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("PhoneNumber");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetails", (string)null);
                });

            modelBuilder.Entity("HRMS.DBL.Entities.Employee", b =>
                {
                    b.HasOne("HRMS.DBL.Entities.EmployeeStatus", "EmployeeStatus")
                        .WithMany()
                        .HasForeignKey("EmployeeStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRMS.DBL.Entities.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("HRMS.DBL.Entities.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HRMS.DBL.Entities.EmployeeDetail", b =>
                {
                    b.HasOne("HRMS.DBL.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRMS.DBL.Entities.User", b =>
                {
                    b.HasOne("HRMS.DBL.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HRMS.DBL.Entities.UserDetail", b =>
                {
                    b.HasOne("HRMS.DBL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HRMS.DBL.Entities.User", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
