using Control.Domain.QueryFilters.Pagination;
using System;

namespace Control.Domain.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}