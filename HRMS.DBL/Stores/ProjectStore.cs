using HRMS.Core.Models.Project;
using HRMS.DBL.DbContextConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Transactions;
using HRMS.Core.Consts;
using HRMS.DBL.Entities;
using System.Linq;
using System.Collections.Generic;

namespace HRMS.DBL.Stores
{
    public class ProjectStore : BaseStore
    {
        public ProjectStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<int?> AddOrUpdateProjectDetails(ProjectRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Projects.Include(x => x.ProjectManagers).FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Id == model.Id);
                if (data == null)
                {
                    data = new Project();
                }

                data.Name = model.Name;
                data.Type = model.Type;
                data.Description = model.Description;

                AddManyToManyData(data, model);
                if (data.Id == 0)
                {
                    await _dbContext.Projects.AddAsync(data);
                }
                else
                {
                    _dbContext.Entry(data).State = EntityState.Modified;
                }

                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IQueryable<Project>> GetProjectDetail(int? projectId)
        {
            return _dbContext.Projects.Include(x => x.ProjectManagers)
                                      .Include(x => x.ProjectManagers).ThenInclude(x => x.Employee)
                                      .Include(x => x.ProjectManagers).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                .Where(x => x.RecordStatus == RecordStatus.Active && x.Id == projectId).AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<Project>> GetProjectDetails(ProjectFilterModel filter)
        {
            return _dbContext.Projects.Include(x => x.ProjectManagers).ThenInclude(x => x.Employee)
                                      .Include(x => x.ProjectManagers).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                            .Where(x => x.RecordStatus == RecordStatus.Active
                                                    && (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                                    (x.Name.Contains(filter.SearchString) || x.Type.Contains(filter.SearchString) || x.Description.Contains(filter.SearchString))))))
                                            .AsNoTracking().AsSplitQuery().OrderByDescending(x => x.CreatedDateTimeUtc);
        }
        public async Task<bool> DeleteProjects(IEnumerable<int> projectIds)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Projects.Include(x => x.ProjectManagers).Where(x => projectIds.Contains(x.Id)).ToListAsync();
                if (data == null || (data != null && data.Count() == 0))
                {
                    return false;
                }

                foreach (var item in data)
                {
                    item.RecordStatus = RecordStatus.InActive;
                    _dbContext.Entry(item).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }

                transaction.Complete();
                return true;
            }
        }
        public async Task<bool> SetProjectsStatus(IEnumerable<ProjectsStatusModel> model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var item in model)
                {
                    var data = await _dbContext.Projects.Include(x => x.ProjectManagers).FirstOrDefaultAsync(x => x.Id == item.Id);
                    if (data == null)
                    {
                        continue;
                    }

                    data.RecordStatus = (item.status ? RecordStatus.Active : RecordStatus.InActive);
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }

                transaction.Complete();
                return true;
            }
        }
        private void AddManyToManyData(Project enitity, ProjectRequestModel model)
        {
            if (model.ProjectManagerIds != null && model.ProjectManagerIds.Any())
            {
                if (enitity?.ProjectManagers != null)
                {
                    _dbContext.ProjectManagers?.RemoveRange(enitity?.ProjectManagers);
                }
                foreach (var item in model.ProjectManagerIds)
                {
                    enitity.ProjectManagers.Add(new ProjectManager { EmployeeId = item, ProjectId = enitity.Id });
                }
            }
        }
    }
}
