using CodeFirst.Domain.Enums;

namespace CodeFirst.Core.DTOs.Course.Response
{
    public class CourseDtoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Estado StateId { get; set; }
    }
}
