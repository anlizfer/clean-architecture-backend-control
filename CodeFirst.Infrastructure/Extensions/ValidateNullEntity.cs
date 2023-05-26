using CodeFirst.Domain.Exceptions;
using System.Linq;

namespace CodeFirst.Infrastructure.Extensions
{
    public static class ValidateNullEntity<T>
    {
        public static void IsNullEntity(T entity)
        {
            if (entity == null) { throw new InfrastructureException("{0} no puede ser nulo.", nameof(entity)); }
        }

        public static void IsNullListEntity(IQueryable<T> entity)
        {
            if (entity?.Any() != true) { throw new InfrastructureException("La lista de {0} no puede ser nulo.", nameof(entity)); }
        }
    }
}