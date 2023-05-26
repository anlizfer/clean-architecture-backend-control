using CodeFirst.Domain.BaseInterfaces;

namespace CodeFirst.Domain.BaseEntities
{
    public abstract class EntityBase : IEntityBase
    {
        public long Id { get; set; }
    }
}