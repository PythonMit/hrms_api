using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HRMS.DBL.Interfaces
{
    public interface IIdentityEntity
    {
        int Id { get; set; }
    }

    public class IdentityEntityComparer : IEqualityComparer<IIdentityEntity>
    {
        public bool Equals([AllowNull] IIdentityEntity x, [AllowNull] IIdentityEntity y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] IIdentityEntity obj)
        {
            return obj.Id;
        }
    }
}
