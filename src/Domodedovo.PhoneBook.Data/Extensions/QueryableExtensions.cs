using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domodedovo.PhoneBook.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> SortBy<TEntity, TKey>(this IQueryable<TEntity> query,
            Expression<Func<TEntity, TKey>> keySelector, bool isFirstSorting = true, bool isDescending = false)
        {
            if (isFirstSorting)
            {
                return isDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
            }

            return isDescending
                ? ((IOrderedQueryable<TEntity>) query).ThenByDescending(keySelector)
                : ((IOrderedQueryable<TEntity>) query).ThenBy(keySelector);
        }
    }
}