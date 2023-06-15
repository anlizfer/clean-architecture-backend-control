using Control.Core.Interfaces.Repositories;
using Control.Domain.Exceptions;
using Control.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Control.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepositoryOLEDB
    {

        private readonly ControlContext _dbContext;
        public GenericRepository(ControlContext dbContext)
        {
            _dbContext = dbContext ?? throw new InfrastructureException(nameof(dbContext), $"El parametro dbContext no puede ser null");
        }        

        public virtual async Task<DataTable> ExecStoreSQLAsync(string? query)
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                var dt = new DataTable();
                command.CommandText = query!;
                _dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                    return dt;
                }
            }
        }

    }
}