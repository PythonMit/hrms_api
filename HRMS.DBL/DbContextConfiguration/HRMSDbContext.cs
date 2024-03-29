using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Consts;
using HRMS.DBL.Entities;

namespace HRMS.DBL.DbContextConfiguration
{
    public class HRMSDbContext : DbContext
    {
        private readonly IUserContextAccessor _userContextAccessor;

        public HRMSDbContext(DbContextOptions<HRMSDbContext> options, IUserContextAccessor userContextAccessor) : base(options)
        {
            _userContextAccessor = userContextAccessor;
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<EmployeeContractStatus> EmployeeContractStatus { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EmployeeOverTimeStatus> EmployeeOverTimeStatus { get; set; }
        public virtual DbSet<DesignationType> DesignationTypes { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<EmployeeStatus> EmployeeStatus { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public virtual DbSet<EmployeeEarningGrossStatus> EmployeeEarningGrossStatus { get; set; }
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public virtual DbSet<EmployeeContract> EmployeeContracts { get; set; }
        public virtual DbSet<EmployeeLeaveStatus> EmployeeLeaveStatus { get; set; }
        public virtual DbSet<EmployeeFixGross> EmployeeFixGross { get; set; }
        public virtual DbSet<EmployeeEarningGross> EmployeeEarningGross { get; set; }
        public virtual DbSet<EmployeeOverTime> EmployeeOverTimes { get; set; }
        public virtual DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
        public virtual DbSet<EmployeeLeaveApplication> EmployeeLeaveApplications { get; set; }
        public virtual DbSet<EmployeeLeaveApplicationComment> EmployeeLeaveApplicationComments { get; set; }
        public virtual DbSet<SystemFlag> SystemFlags { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<EmployeeSalaryStatus> EmployeeSalaryStatus { get; set; }
        public virtual DbSet<ImagekitDetail> ImagekitDetails { get; set; }
        public virtual DbSet<EmployeeOverTimeManager> EmployeeOverTimeManagers { get; set; }
        public virtual DbSet<EmployeeLeaveApplicationManager> EmployeeLeaveApplicationManagers { get; set; }
        public virtual DbSet<EmployeeIncentiveDetails> EmployeeIncentiveDetails { get; set; }
        public virtual DbSet<EmployeeIncentiveStatus> EmployeeIncentiveStatus { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectManager> ProjectManagers { get; set; }
        public virtual DbSet<LeaveCategory> LeaveCategories { get; set; }
        public virtual DbSet<EmployeeLeaveTransaction> EmployeeLeaveTransactions { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<EmployeeFNFDetails> EmployeeFNFDetails { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<EmployeeHoldSalary> EmployeeHoldSalaries { get; set; }
        public virtual DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceUserHistory> ResourceUserHistories { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<TrackableEntity>())
            {
                if (_userContextAccessor?.UserId != -1)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.RecordStatus = RecordStatus.Active;
                            entry.Entity.CreatedByUserId = _userContextAccessor.UserId;
                            entry.Entity.CreatedDateTimeUtc = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedByUserId = _userContextAccessor.UserId;
                            entry.Entity.UpdatedDateTimeUtc = DateTime.UtcNow;
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
