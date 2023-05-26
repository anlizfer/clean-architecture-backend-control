using CodeFirst.Domain.BaseInterfaces;
using System;

namespace CodeFirst.Domain.BaseEntities
{
    public abstract class AuditEntity : DeleteEntity, IAuditEntity, IEntityBase
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}