using CodeFirst.Domain.BaseEntities;
using System;
using System.Collections.Generic;

namespace CodeFirst.Domain.Entities
{
    public class Student : EntityBase
    {
        public Student()
        {
            Inscriptions = new HashSet<Inscription>();
        }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Inscription> Inscriptions { get; set; }
    }
}