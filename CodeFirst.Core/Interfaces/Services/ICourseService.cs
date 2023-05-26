using CodeFirst.Core.DTOs.Course.Response;
using CodeFirst.Core.DTOs.Request.Course.Requests;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface ICourseService
    {
        PagedResponse<IEnumerable<CourseDtoResponse>> GetCourses(CourseQueryFilter filters, string actionUrl);

        Task<Response<CourseDtoResponse>> GetCourseAsync(long id);

        Task<Response<CourseDtoResponse>> AddCourseAsync(CourseAddDtoRequest Course);

        Task<Response<CourseDtoResponse>> UpdateCourseAsync(long id, CourseUpdateDtoRequest Course);

        Task<Response<bool>> DeleteCourseAsync(long id);
    }
}