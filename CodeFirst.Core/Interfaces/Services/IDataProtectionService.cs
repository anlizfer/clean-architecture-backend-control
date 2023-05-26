using CodeFirst.Core.DTOs.DataProtection.Responses;
using CodeFirst.Domain.Wrappers;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IDataProtectionService
    {
        Task<Response<HashDtoResponse>> RealizarHash(string textoPlano);
        Task<Response<EncriptarDtoResponse>> Encriptar(string textoPlano);
    }
}
