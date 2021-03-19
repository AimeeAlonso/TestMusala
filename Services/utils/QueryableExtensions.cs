using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Gateway.Dtos;

namespace Services.Utils
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, PaginationInfoDto pageInfo)
        {
            var total = source.Count();
            pageInfo.Total = total;
            var take = pageInfo.PageSize;
            var skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
            
            if (take > total)
            {
                take = total;
                skip = 0;
            }

            if (skip > total)
            {
                skip = total;
                take = 0;
            }

            if (skip + take > total)
            {
                take = total - skip;
            }
            
            return source.Skip( skip ).Take( take );
        }
        
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName, string direction)
        {
            if (direction == "asc")
            {
                return source.OrderBy(t => EF.Property<string>(t, propertyName));
            }
            else
            {
                return source.OrderByDescending(t => EF.Property<string>(t, propertyName));
            }
        } 
        
        public static IQueryable<T> FilterBy<T>(this IQueryable<T> source, string propertyName, string term)
        {
            return source.Where(t => EF.Property<string>(t, propertyName).Contains(term));
        } 
    }
}