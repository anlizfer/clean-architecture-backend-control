using CodeFirst.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface ISqlExampleService
    {
        Task<Response<String>> GetSQLResult();
    }
}
