using CodeFirst.Core.DTOs.Account.Requests;
using CodeFirst.Core.DTOs.Account.Responses;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IAccountService
    {
        PagedResponse<IEnumerable<UsuarioDtoResponse>> GetUsers(UserQueryFilter filters, string actionUrl);
        Task<Response<AuthenticationDtoResponse>> GetRenovateTokenAsync(HttpContext httpContext);
        Task<Response<AuthenticationDtoResponse>> AddAccountAsync(CredentialsUserDtoRequest credentials);
        Task<Response<AuthenticationDtoResponse>> GetLoginAsync(CredentialsUserDtoRequest credentials);
        Task<Response<bool>> CrearClaimUser(AdminAddDtoRequest AdminDTO);
        Task<Response<bool>> RemoverClaimUser(AdminUpdateDtoRequest AdminDTO);
    }
}
