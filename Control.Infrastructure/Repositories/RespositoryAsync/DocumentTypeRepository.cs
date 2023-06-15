using Control.Domain.Entities;
using Control.Infrastructure.Settings;

namespace Control.Infrastructure.Repositories.RespositoryAsync
{
    public class DocumentTypeRepositoryAsync : GenericRepository<DocumentType>
    {
        public DocumentTypeRepositoryAsync(ControlContext ControlContext) : base(ControlContext)
        {
        }
    }
}