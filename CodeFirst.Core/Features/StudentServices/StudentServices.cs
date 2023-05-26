using AutoMapper;
using CodeFirst.Core.DTOs.Student.Request;
using CodeFirst.Core.DTOs.Student.Response;
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

namespace CodeFirst.Core.Features.StudentServices
{
    public class StudentServices : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly PaginationOptionsSetting _paginationOptions;
        public StudentServices(
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

        public PagedResponse<IEnumerable<StudentDtoResponse>> GetStudents(StudentQueryFilter filters, string actionUrl)
        {
            PaginationFilter validFilter = new(filters.PageNumber, filters.PageSize, _paginationOptions);
            IEnumerable<Student> StudentsPagedData = _unitOfWork.StudentRepositoryAsync
                                                                .GetPagedElementsAsync(
                                                                                        validFilter.PageNumber,
                                                                                        validFilter.PageSize,
                                                                                        x => x.Id,
                                                                                        true).Result;

            if (!string.IsNullOrEmpty(filters.Name))
            {
                StudentsPagedData = StudentsPagedData.Where(x => x.Name == filters.Name);
            }

            if (filters.DateOfBirth != default)
            {
                StudentsPagedData = StudentsPagedData.Where(x => x.DateOfBirth.ToShortDateString() == filters.DateOfBirth.ToShortDateString());
            }
            var total = _unitOfWork.StudentRepositoryAsync.GetCountAsync().Result;

            PagedResponse<IEnumerable<StudentDtoResponse>> response = PaginationHelper.PadageCreateResponse<StudentDtoResponse, Student>(
                                                                    StudentsPagedData.ToList(),
                                                                    validFilter,
                                                                    _paginationOptions,
                                                                    total,
                                                                    _uriService,
                                                                    actionUrl,
                                                                    _mapper
                                                               );
            return response;
        }

        public async Task<Response<StudentDtoResponse>> GetStudentAsync(long id)
        {
            Student StudentBuscado = await _unitOfWork.StudentRepositoryAsync.GetByIdAsync(id).ConfigureAwait(false);
            if (StudentBuscado == null) { throw new CoreException("La información solicitada no exitosa."); }
            StudentDtoResponse StudentMap = _mapper.Map<StudentDtoResponse>(StudentBuscado);
            return new Response<StudentDtoResponse>(StudentMap) { Message = "La información solicitada ha sido exitosa." };
        }

        public async Task<Response<StudentDtoResponse>> AddStudentAsync(StudentAddDtoRequest Student)
        {
            Student StudentMap = _mapper.Map<Student>(Student);
            await _unitOfWork.StudentRepositoryAsync.AddAsync(StudentMap).ConfigureAwait(false);
            await _unitOfWork.CommitAsync();
            StudentDtoResponse StudentCreado = _mapper.Map<StudentDtoResponse>(StudentMap);
            return new Response<StudentDtoResponse>(StudentCreado) { Message = $"El Student {StudentCreado.Name} ha sido creado." };
        }

        public async Task<Response<StudentDtoResponse>> UpdateStudentAsync(long id, StudentUpdateDtoRequest Student)
        {
            Student StudentBuscado = await _unitOfWork.StudentRepositoryAsync
                                                    .GetFirstAsync(x => x.Id.Equals(id))
                                                    .ConfigureAwait(false);
            if (StudentBuscado == null) { throw new CoreException("La información solicitada no exitosa."); }

            StudentBuscado.Name = Student.Name;

            await _unitOfWork.StudentRepositoryAsync.UpdateAsync(StudentBuscado);
            await _unitOfWork.CommitAsync();
            StudentDtoResponse StudentActualizado = _mapper.Map<StudentDtoResponse>(StudentBuscado);

            return new Response<StudentDtoResponse>(StudentActualizado) { Message = $"El Student {StudentActualizado.Name} ha sido actualizada." };
        }

        public async Task<Response<bool>> DeleteStudentAsync(long id)
        {
            if (id <= 0) { throw new CoreException($"El valor del identificador id debe ser superior a cero(0)."); }
            bool StudentEliminado = await _unitOfWork.StudentRepositoryAsync.DeleteAsync(id).ConfigureAwait(false);
            if (!StudentEliminado) { throw new CoreException("El registro no ha sido Eliminado."); }
            await _unitOfWork.CommitAsync();
            return new Response<bool>(StudentEliminado) { Message = $"El registro solicitado ha sido eliminado." };
        }
    }
}