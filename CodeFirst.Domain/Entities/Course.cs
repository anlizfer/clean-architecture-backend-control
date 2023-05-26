using CodeFirst.Domain.BaseEntities;
using CodeFirst.Domain.Enums;
using System.Collections.Generic;

namespace CodeFirst.Domain.Entities
{
    public class Course : AuditEntity
    {
        public Course()
        {
            Inscriptions = new HashSet<Inscription>();
        }

        public string Name { get; set; }

        public Estado StateId { get; set; }

        public virtual ICollection<Inscription> Inscriptions { get; set; }
    }
}