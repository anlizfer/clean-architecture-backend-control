using CodeFirst.Core.DTOs.Account.Requests;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeFirst.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// lista de roles.
        /// </summary>
        /// <remarks>
        /// Obtiene la lista de roles.
        /// </remarks>
        /// <returns>Retorna una lista de roles.</returns>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("Roles")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleService.GetRolesAllAsync());
        }

        /// <summary>
        /// Asignar rol al usuario.
        /// </summary>
        /// <remarks>
        /// Ingresar el rol al usuario.
        /// </remarks>
        /// <returns>Retorna boleano.</returns>
        /// <param name="rol">El objeto roles.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("AsignarRol")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AsignarRol(RolDtoRequest rol)
        {
            return Ok(await _roleService.AsignarRol(rol));
        }

        /// <summary>
        /// Remover rol.
        /// </summary>
        /// <remarks>
        /// Eliminar roles.
        /// </remarks>
        /// <returns>Retorna un boleano.</returns>
        /// <param name="rol">El objeto roles.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("RemoveRol")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoverRol(RolDtoRequest rol)
        {
            return Ok(await _roleService.RemoverRol(rol));
        }
    }
}
