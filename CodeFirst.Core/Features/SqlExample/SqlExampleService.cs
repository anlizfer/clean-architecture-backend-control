using CodeFirst.Core.DTOs.Course.Response;
using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Domain.Wrappers;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Core.Features.SqlExample
{
    public class SqlExampleService
    {
        private readonly IGenericRepositoryOLEDB RepositoryOled;

        public SqlExampleService(IGenericRepositoryOLEDB _RepositoryOled)
        {
            RepositoryOled = _RepositoryOled;
        }

        public async Task<Response<String>> GetSQLResult()
        {
            var result=await RepositoryOled.ExecStoreSQLAsync("SELECT * FROM table");
            return new Response<String>("Cadena respuesta") { Message = "La información solicitada ha sido exitosa." };
        }

    }
}
