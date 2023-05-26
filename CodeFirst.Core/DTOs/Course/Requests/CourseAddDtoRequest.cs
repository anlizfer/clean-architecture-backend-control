using CodeFirst.Domain.Enums;

namespace CodeFirst.Core.DTOs.Request.Course.Requests
{
    public class CourseAddDtoRequest
    {
        public string Name { get; set; }

        public Estado StateId { get; set; }
    }
}