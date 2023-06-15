using Control.Core.DTOs.DocumentType.Response;
using Control.Domain.Wrappers;
using System.Threading.Tasks;

namespace Control.Core.Interfaces.Services
{
    public interface IDocumentTypeService
    {
        Task<Response<DocumentTypeDtoResponse>> GetTypeDocumentAsync(long id);
    }
}
