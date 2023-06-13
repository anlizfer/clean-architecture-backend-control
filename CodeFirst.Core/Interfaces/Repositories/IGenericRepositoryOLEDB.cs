using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Repositories
{
    public interface IGenericRepositoryOLEDB
    {
        Task<DataTable> ExecStoreSQLAsync(string? query);
    }
}
