using Control.Domain.BaseInterfaces;

namespace Control.Domain.BaseEntities
{
    public abstract class DeleteEntity : EntityBase, IDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}