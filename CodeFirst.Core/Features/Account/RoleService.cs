using CodeFirst.Core.DTOs.Account.Requests;
using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodeFirst.Core.Features.Account
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleService(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response<IEnumerable<string>>> GetRolesAllAsync()
        {
            var listRoles = (await _unitOfWork.RoleRepositoryAsync.GetAllAsync()).Select(x => x.Name).ToList();
            return new Response<IEnumerable<string>>(listRoles) { Message = "La información solicitada ha sido exitosa." };
        }
        public async Task<Response<bool>> AsignarRol(RolDtoRequest rol)
        {
            var user = await _userManager.FindByIdAsync(rol.UsuarioId);
            if (user == null) { throw new CoreException("La información solicitada no exitosa."); }

            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, rol.NombreRol));
            return new Response<bool>(true) { Message = "La información solicitada ha sido exitosa." };
        }
        public async Task<Response<bool>> RemoverRol(RolDtoRequest rol)
        {
            var user = await _userManager.FindByIdAsync(rol.UsuarioId);
            if (user == null) { throw new CoreException("La información solicitada no exitosa."); }

            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, rol.NombreRol));
            return new Response<bool>(true) { Message = "La información solicitada ha sido exitosa." };
        }
    }
}
