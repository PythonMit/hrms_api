using System;
using System.Collections.Generic;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class QueriableExtensions
    {
        public static IEnumerable<T> SelectRecursive<T>(this IQueryable<T> source, Func<T, IQueryable<T>> selector)
        {
            foreach (var parent in source)
            {
                yield return parent;

                var children = selector(parent);
                foreach (var child in children.SelectRecursive(selector))
                    yield return child;
            }
        }
    }
}
