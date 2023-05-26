using CodeFirst.Core.DTOs.DataProtection.Responses;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Helpers;
using CodeFirst.Domain.Wrappers;
using System.Threading.Tasks;

namespace CodeFirst.Core.Features.DataProtectionService
{
    public class DataProtectionService : IDataProtectionService
    {
        private readonly DataProtection _dataProtection;
        public DataProtectionService(DataProtection dataProtection)
        {
            _dataProtection = dataProtection;
        }
        public async Task<Response<EncriptarDtoResponse>> Encriptar(string textoPlano)
        {
            EncriptarDtoResponse encriptar = new();
            encriptar.TextoPlano = textoPlano;
            encriptar.TextoCifrado = _dataProtection.EncryptData(textoPlano);
            encriptar.TextoDesencriptado = _dataProtection.DecryptData(encriptar.TextoCifrado);

            return await Task.FromResult(new Response<EncriptarDtoResponse>(encriptar) { Message = "La información solicitada ha sido exitosa." });
        }

        public async Task<Response<HashDtoResponse>> RealizarHash(string textoPlano)
        {
            HashDtoResponse hash = new();
            hash.TextoPlano = textoPlano;
            hash.Hash = DataProtection.Hash(textoPlano);

            return await Task.FromResult(new Response<HashDtoResponse>(hash) { Message = "La información solicitada ha sido exitosa." });
        }
    }
}
