using HRMS.DBL.DbContextConfiguration;

namespace HRMS.DBL.Stores
{
    public class ReportStore : BaseStore
    {
        public ReportStore(HRMSDbContext dbContext) : base(dbContext) { }
    }
}
