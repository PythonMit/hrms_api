using HRMS.Api.Controllers.Base;
using HRMS.Core.Consts;
using HRMS.DBL.DbContextConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/infrastructure"), Tags("Infrastructure")]
    public class InfrastructureController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HRMSDbContext _HRMSDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="HRMSDbContext"></param>
        public InfrastructureController(IConfiguration configuration, HRMSDbContext HRMSDbContext)
        {
            _configuration = configuration;
            _HRMSDbContext = HRMSDbContext;
        }

        /// <summary>
        /// Migrate Database manually
        /// </summary>
        /// <returns></returns>
        [HttpGet("db/migrate"), Base.AuthorizeAttribute(RoleTypes.SuperAdmin)]
        public async Task<IActionResult> MigrateDb()
        {
            var defaultConnectionString = _configuration.GetConnectionString("DefaultConnection");
            _HRMSDbContext.Database.SetConnectionString(defaultConnectionString);
            await _HRMSDbContext.Database.MigrateAsync();
            _HRMSDbContext.ChangeTracker.Clear();

            return Success();
        }
    }
}
