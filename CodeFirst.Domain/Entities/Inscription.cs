using CodeFirst.Domain.BaseEntities;

namespace CodeFirst.Domain.Entities
{
    public class Inscription : EntityBase
    {
        public long CourseId { get; set; }

        public long StudentId { get; set; }

        public virtual Course Courses { get; set; }
        public virtual Student Students { get; set; }
    }
}