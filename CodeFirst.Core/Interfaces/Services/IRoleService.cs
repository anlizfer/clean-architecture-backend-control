using CodeFirst.Core.DTOs.Account.Requests;
using CodeFirst.Domain.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<Response<IEnumerable<string>>> GetRolesAllAsync();
        Task<Response<bool>> AsignarRol(RolDtoRequest rol);
        Task<Response<bool>> RemoverRol(RolDtoRequest rol);
    }
}
