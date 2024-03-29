using HRMS.DBL.DbContextConfiguration;

namespace HRMS.DBL.Stores
{
    public class BaseStore
    {
        protected readonly HRMSDbContext _dbContext;

        public BaseStore(HRMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
