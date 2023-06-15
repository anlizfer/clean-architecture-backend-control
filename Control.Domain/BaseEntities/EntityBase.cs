using Control.Domain.BaseInterfaces;

namespace Control.Domain.BaseEntities
{
    public abstract class EntityBase : IEntityBase
    {
        public long Id { get; set; }
    }
}