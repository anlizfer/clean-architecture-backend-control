using CodeFirst.Core.QueryFilters.Pagination;
using System;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
