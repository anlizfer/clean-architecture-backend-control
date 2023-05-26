using CodeFirst.Domain.QueryFilters.Pagination;
using System;

namespace CodeFirst.Domain.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}