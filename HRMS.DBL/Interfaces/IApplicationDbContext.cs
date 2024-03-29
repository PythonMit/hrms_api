using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRMS.DBL.Entities;

namespace HRMS.DBL.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }

        IQueryable<T> GetReadOnlyEntity<T>() where T : class;

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
#pragma warning disable S3427 // Method overloads with default parameter values should not overlap 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
#pragma warning restore S3427 // Method overloads with default parameter values should not overlap 


    }
}
