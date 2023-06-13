using CodeFirst.Domain.Entities;
using CodeFirst.Infrastructure.Settings;

namespace CodeFirst.Infrastructure.Repositories.RespositoryAsync
{
    public class DocumentTypeRepositoryAsync : GenericRepository<DocumentType>
    {
        public DocumentTypeRepositoryAsync(CodeFirstContext codeFirstContext) : base(codeFirstContext)
        {
        }
    }
}