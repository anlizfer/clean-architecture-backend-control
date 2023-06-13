using CodeFirst.Core.DTOs.DocumentType.Response;
using CodeFirst.Domain.Wrappers;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IDocumentTypeService
    {
        Task<Response<DocumentTypeDtoResponse>> GetTypeDocumentAsync(long id);
    }
}
