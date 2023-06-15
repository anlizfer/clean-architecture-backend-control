using Control.Core.QueryFilters.Pagination;
using System;

namespace Control.Core.Interfaces.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
