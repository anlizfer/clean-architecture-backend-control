using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CodeFirst.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepositoryOLEDB
    {

        private readonly CodeFirstContext _dbContext;
        public GenericRepository(CodeFirstContext dbContext)
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