using CodeFirst.Domain.Interfaces;
using CodeFirst.Domain.QueryFilters.Pagination;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace CodeFirst.Core.Features.Pagination
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            Uri _enpointUri = new(string.Concat(_baseUri, route));
            string modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}