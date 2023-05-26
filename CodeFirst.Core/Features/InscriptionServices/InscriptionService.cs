using AutoMapper;
using CodeFirst.Core.DTOs.Request.Inscription.Requests;
using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Entities;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Domain.Helpers;
using CodeFirst.Domain.Interfaces;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.QueryFilters.Pagination;
using CodeFirst.Domain.Settings;
using CodeFirst.Domain.Wrappers;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Core.Features.InscriptionServices
{
    public class InscriptionService : IInscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly PaginationOptionsSetting _paginationOptions;

        public InscriptionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUriService uriService,
            IOptions<PaginationOptionsSetting> options
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uriService = uriService;
            _paginationOptions = options.Value;
        }

        public async Task<Response<bool>> DeleteInscriptionAsync(int id)
        {
            bool InscriptionEliminado = await _unitOfWork.InscriptionRepositoryAsync.DeleteAsync(id).ConfigureAwait(false);
            if (!InscriptionEliminado) { throw new CoreException("Inscription no Eliminado."); }
            return new Response<bool>(InscriptionEliminado);
        }

        public async Task<Response<Inscription>> GetInscriptionAsync(long id)
        {
            Inscription Inscription = await _unitOfWork.InscriptionRepositoryAsync
                                                       .GetFirstAsync(x => x.Id.Equals(id))
                                                       .ConfigureAwait(false);
            if (Inscription == null) { throw new CoreException("Inscription no Encontrado."); }
            return new Response<Inscription>(Inscription);
        }

        public PagedListResponse<List<Inscription>> GetInscriptions(InscriptionQueryFilter filters, string actionUrl)
        {
            System.Reflection.PropertyInfo propertyInfo = typeof(Inscription).GetProperty("nombre");
            IQueryable<Inscription> InscriptionsPagedData = (IQueryable<Inscription>)_unitOfWork.InscriptionRepositoryAsync.GetPagedElementsAsync(filters.PageNumber, filters.PageSize, x => propertyInfo.GetValue(x, null), true);

            PaginationFilter validFilter = new(filters.PageNumber, filters.PageSize, _paginationOptions);

            if (filters.CourseId != default)
            {
                InscriptionsPagedData = InscriptionsPagedData.Where(x => x.CourseId == filters.CourseId);
            }

            if (filters.StudentId != default)
            {
                InscriptionsPagedData = InscriptionsPagedData.Where(x => x.StudentId == filters.StudentId);
            }

            PagedListResponse<List<Inscription>> pagedReponse = PaginationHelper.CreatePagedReponse<Inscription>(InscriptionsPagedData.ToList(), validFilter, _paginationOptions, InscriptionsPagedData.Count(), _uriService, actionUrl);
            return pagedReponse;
        }

        public async Task<Response<Inscription>> AddInscriptionAsync(InscriptionAddDtoRequest Inscription)
        {
            Inscription InscriptionMap = _mapper.Map<Inscription>(Inscription);
            await _unitOfWork.InscriptionRepositoryAsync.AddAsync(InscriptionMap).ConfigureAwait(false);
            return new Response<Inscription>(InscriptionMap);
        }

        public async Task<Response<bool>> UpdateInscriptionAsync(InscriptionUpdateDtoRequest Inscription)
        {
            Response<Inscription> InscriptionUpdate = await GetInscriptionAsync(Inscription.Id).ConfigureAwait(false);

            return new Response<bool>(InscriptionUpdate != null);
        }
    }
}