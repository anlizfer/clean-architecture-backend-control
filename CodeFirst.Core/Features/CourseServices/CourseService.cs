using AutoMapper;
using CodeFirst.Core.DTOs.Course.Response;
using CodeFirst.Core.DTOs.Request.Course.Requests;
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

namespace CodeFirst.Core.Features.CourseServices
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly PaginationOptionsSetting _paginationOptions;

        public CourseService(
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
        public PagedResponse<IEnumerable<CourseDtoResponse>> GetCourses(CourseQueryFilter filters, string actionUrl)
        {
            PaginationFilter validFilter = new(filters.PageNumber, filters.PageSize, _paginationOptions);
            IEnumerable<Course> CoursesPagedData = _unitOfWork.CourseRepositoryAsync
                                                                .GetPagedElementsAsync(
                                                                                        validFilter.PageNumber,
                                                                                        validFilter.PageSize,
                                                                                        x => x.Id,
                                                                                        true).Result;

            if (!string.IsNullOrEmpty(filters.Name))
            {
                CoursesPagedData = CoursesPagedData.Where(x => x.Name == filters.Name);
            }
            var total = _unitOfWork.CourseRepositoryAsync.GetCountAsync().Result;

            PagedResponse<IEnumerable<CourseDtoResponse>> response = PaginationHelper.PadageCreateResponse<CourseDtoResponse, Course>(
                                                                    CoursesPagedData.ToList(),
                                                                    validFilter,
                                                                    _paginationOptions,
                                                                    total,
                                                                    _uriService,
                                                                    actionUrl,
                                                                    _mapper
                                                               );
            return response;
        }
        public async Task<Response<CourseDtoResponse>> GetCourseAsync(long id)
        {
            Course CourseBuscado = await _unitOfWork.CourseRepositoryAsync.GetByIdAsync(id).ConfigureAwait(false);
            if (CourseBuscado == null) { throw new CoreException("La información solicitada no exitosa."); }
            CourseDtoResponse CourseMap = _mapper.Map<CourseDtoResponse>(CourseBuscado);
            return new Response<CourseDtoResponse>(CourseMap) { Message = "La información solicitada ha sido exitosa." };
        }

        public async Task<Response<CourseDtoResponse>> AddCourseAsync(CourseAddDtoRequest Course)
        {
            Course CourseMap = _mapper.Map<Course>(Course);
            await _unitOfWork.CourseRepositoryAsync.AddAsync(CourseMap).ConfigureAwait(false);
            await _unitOfWork.CommitAsync();
            CourseDtoResponse CourseCreado = _mapper.Map<CourseDtoResponse>(CourseMap);
            return new Response<CourseDtoResponse>(CourseCreado) { Message = $"El Course {CourseCreado.Name} ha sido creado." };
        }

        public async Task<Response<CourseDtoResponse>> UpdateCourseAsync(long id, CourseUpdateDtoRequest Course)
        {
            Course CourseBuscado = await _unitOfWork.CourseRepositoryAsync
                                                    .GetFirstAsync(x => x.Id.Equals(id))
                                                    .ConfigureAwait(false);
            if (CourseBuscado == null) { throw new CoreException("La información solicitada no exitosa."); }

            CourseBuscado.Name = Course.Name;

            await _unitOfWork.CourseRepositoryAsync.UpdateAsync(CourseBuscado);
            await _unitOfWork.CommitAsync();
            CourseDtoResponse CourseActualizado = _mapper.Map<CourseDtoResponse>(CourseBuscado);

            return new Response<CourseDtoResponse>(CourseActualizado) { Message = $"El Curso {CourseActualizado.Name} ha sido actualizada." };
        }
        public async Task<Response<bool>> DeleteCourseAsync(long id)
        {
            if (id <= 0) { throw new CoreException($"El valor del identificador id debe ser superior a cero(0)."); }
            bool CourseEliminado = await _unitOfWork.CourseRepositoryAsync.DeleteAsync(id).ConfigureAwait(false);
            if (!CourseEliminado) { throw new CoreException("El registro no ha sido Eliminado."); }
            await _unitOfWork.CommitAsync();
            return new Response<bool>(CourseEliminado) { Message = $"El registro solicitado ha sido eliminado." };
        }
    }
}