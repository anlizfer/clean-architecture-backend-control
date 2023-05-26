using CodeFirst.Domain.BaseInterfaces;

namespace CodeFirst.Domain.BaseEntities
{
    public abstract class DeleteEntity : EntityBase, IDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}