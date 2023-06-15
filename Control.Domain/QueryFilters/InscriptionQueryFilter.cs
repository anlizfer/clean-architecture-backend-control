namespace Control.Domain.QueryFilters
{
    public class InscriptionQueryFilter
    {
        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}