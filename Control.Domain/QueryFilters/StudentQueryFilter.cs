using System;

namespace Control.Domain.QueryFilters
{
    public class StudentQueryFilter
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}