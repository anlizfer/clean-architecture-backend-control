using Control.Domain.Entities;
using Control.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Core.Interfaces.Services
{
    public interface ISqlExampleService
    {
        Task<Response<IEnumerable<DocumentType>>> GetSQLResult();
    }
}
