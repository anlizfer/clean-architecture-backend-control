using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Core.Interfaces.Repositories
{
    public interface IGenericRepositoryOLEDB
    {
        Task<DataTable> ExecStoreSQLAsync(string? query);
    }
}
