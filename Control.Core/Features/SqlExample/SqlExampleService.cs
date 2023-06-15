using Control.Core.Interfaces.Repositories;
using Control.Core.Interfaces.Services;
using Control.Domain.Entities;
using Control.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;

namespace Control.Core.Features.SqlExample
{
    public class SqlExampleService : ISqlExampleService
    {
        private readonly IGenericRepositoryOLEDB RepositoryOled;

        public SqlExampleService(IGenericRepositoryOLEDB repositoryOled)
        {
            RepositoryOled = repositoryOled;
            //RepositoryOled = _RepositoryOled;
        }

        public async Task<Response<IEnumerable<DocumentType>>> GetSQLResult()
        {
            List<DocumentType> listaDocumentType = new List<DocumentType>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var res = await RepositoryOled.ExecStoreSQLAsync("SELECT * FROM DocumentType");
                    foreach (DataRow row in res.Rows)
                    {
                        DocumentType DocumentTypeModel = new DocumentType
                        {
                            IdDocumentType = Convert.ToInt32(row["IdDocumentType"]),
                            NameDocumentType = row["nameDocumentType"].ToString(),
                            Status = Convert.ToBoolean(row["Status"])
                        };

                        listaDocumentType.Add(DocumentTypeModel);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    // Puedes hacer rollback o tomar alguna acción según sea necesario

                    // Realizar rollback explícitamente
                    scope.Dispose();

                    return new Response<IEnumerable<DocumentType>>(null)
                    {
                        Message = "Se produjo un error al realizar la transacción."
                    };
                }
            }

            return new Response<IEnumerable<DocumentType>>(listaDocumentType)
            {
                Message = "La información solicitada ha sido exitosa."
            };
        }


    }
}
