using AutoMapper;
using CodeFirst.Domain.Interfaces;
using CodeFirst.Domain.QueryFilters.Pagination;
using CodeFirst.Domain.Settings;
using CodeFirst.Domain.Wrappers;
using System;
using System.Collections.Generic;

namespace CodeFirst.Domain.Helpers
{
    public static class PaginationHelper
    {
        public static PagedListResponse<List<T>> CreatePagedReponse<T>(
            List<T> pagedData,
            PaginationFilter validFilter,
            PaginationOptionsSetting options,
            int totalRecords,
            IUriService uriService,
            string route)
        {
            PagedListResponse<List<T>> respose = new(pagedData, validFilter.PageNumber, validFilter.PageSize);
            double totalPages = (totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, options), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize, options), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, options), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize, options), route);
            respose.HasPreviousPage = validFilter.PageNumber > 1;
            respose.HasNextPage = validFilter.PageNumber < roundedTotalPages;
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static MetadataSetting CreateMetaData<T>(
            PagedListResponse<List<T>> respose
            )
        {
            var metadata = new MetadataSetting
            {
                TotalCount = respose.TotalRecords,
                PageSize = respose.PageSize,
                CurrentPage = respose.PageNumber,
                TotalPages = respose.TotalPages,
                HasNextPage = respose.HasNextPage,
                HasPreviousPage = respose.HasPreviousPage,
                FirstPageUrl = respose.FirstPage.ToString(),
                LastPageUrl = respose.LastPage.ToString(),
                NextPageUrl = respose.HasNextPage ? respose.NextPage.ToString() : "",
                PreviousPageUrl = respose.HasPreviousPage ? respose.PreviousPage.ToString() : ""
            };
            return metadata;
        }

        public static PagedResponse<IEnumerable<T>> PadageResponse<T>(
            PagedListResponse<List<T>> pagedReponse,
            IMapper _mapper
            )
        {
            var entitiesDtos = _mapper.Map<IEnumerable<T>>(pagedReponse.Data);
            var metadata = CreateMetaData(pagedReponse);
            var response = new PagedResponse<IEnumerable<T>>(entitiesDtos)
            {
                Meta = metadata,
                Message = "La solicitud exitosa."
            };

            return response;
        }

        public static PagedResponse<IEnumerable<T>> PadageCreateResponse<T, T1>(
            List<T1> pagedData,
            PaginationFilter validFilter,
            PaginationOptionsSetting options,
            int totalRecords,
            IUriService uriService,
            string rout,
            IMapper _mapper
            )
        {
            PagedListResponse<List<T1>> pagedReponse = PaginationHelper
                                                .CreatePagedReponse(
                                                    pagedData,
                                                    validFilter,
                                                    options,
                                                    totalRecords,
                                                    uriService, rout
                                                );
            var entitiesDtos = _mapper.Map<IEnumerable<T>>(pagedReponse.Data);
            var metadata = CreateMetaData(pagedReponse);
            var response = new PagedResponse<IEnumerable<T>>(entitiesDtos)
            {
                Meta = metadata,
                Message = "La solicitud exitosa."
            };

            return response;
        }
    }
}