using Control.Domain.Enums;

namespace Control.Domain.QueryFilters
{
    public class CourseQueryFilter
    {
        public string Name { get; set; }

        public Estado StateId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}