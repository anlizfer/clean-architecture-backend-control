using CodeFirst.Core.DTOs.Student.Request;
using CodeFirst.Core.DTOs.Student.Response;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IStudentService
    {
        PagedResponse<IEnumerable<StudentDtoResponse>> GetStudents(StudentQueryFilter filters, string actionUrl);

        Task<Response<StudentDtoResponse>> GetStudentAsync(long id);

        Task<Response<StudentDtoResponse>> AddStudentAsync(StudentAddDtoRequest Student);

        Task<Response<StudentDtoResponse>> UpdateStudentAsync(long id, StudentUpdateDtoRequest Student);

        Task<Response<bool>> DeleteStudentAsync(long id);
    }
}