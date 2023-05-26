using CodeFirst.Core.DTOs.Account.Requests;
using CodeFirst.Core.DTOs.Account.Responses;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CodeFirst.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        public AccountController(IAccountService account)
        {
            _account = account;
        }

        /// <summary>
        /// Obtiene un listado de usuarios.
        /// </summary>
        /// <remarks>
        /// De acuerdo a los filtros se obtiene un listado de usuario.
        /// </remarks>
        /// <returns>Retorna un listado de usuarios solicitado.</returns>
        /// <param name="filters">Diferentes filtros para obtener el listado de usuarios.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("usuarios", Name = nameof(GetAllUsers))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult GetAllUsers([FromQuery] UserQueryFilter filters)
        {
            var students = _account.GetUsers(filters, Url.RouteUrl(nameof(GetAllUsers)).ToString());
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(students.Meta));
            return Ok(students);
        }

        /// <summary>
        /// Registrar una cuenta.
        /// </summary>
        /// <remarks>
        /// Inserta los datos de la cuenta.
        /// </remarks>
        /// <returns>Retorna el token.</returns>
        /// <param name="account">El objeto cuenta.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("registrar")]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterUser([FromBody] CredentialsUserDtoRequest account)
        {
            return Ok(await _account.AddAccountAsync(account));
        }

        /// <summary>
        /// Autenticación del usuario.
        /// </summary>
        /// <remarks>
        /// ingresa las credenciales de la cuenta.
        /// </remarks>
        /// <returns>Retorna el token.</returns>
        /// <param name="credentials">El objeto credenciales usuario.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] CredentialsUserDtoRequest credentials)
        {
            return Ok(await _account.GetLoginAsync(credentials));
        }

        /// <summary>
        /// Renovar token.
        /// </summary>
        /// <remarks>
        /// obtiene la renovacion del token.
        /// </remarks>
        /// <returns>Retorna los datos del token.</returns>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("renovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<AuthenticationDtoResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Renovar()
        {
            return Ok(await _account.GetRenovateTokenAsync(HttpContext));
        }

        /// <summary>
        /// Claim usuario.
        /// </summary>
        /// <remarks>
        /// ingresa el claim del usuario.
        /// </remarks>
        /// <returns>Retorna boleano.</returns>
        /// <param name="AdminDTO">El objeto usuario.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("crearClaimUser")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CrearClaimUser(AdminAddDtoRequest AdminDTO)
        {
            return Ok(await _account.CrearClaimUser(AdminDTO));
        }

        /// <summary>
        /// Autenticación del usuario.
        /// </summary>
        /// <remarks>
        /// ingresa las credenciales de la cuenta.
        /// </remarks>
        /// <returns>Retorna el token.</returns>
        /// <param name="AdminDTO">El objeto credenciales usuario.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("removerClaimUser")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoverClaimUser(AdminUpdateDtoRequest AdminDTO)
        {
            return Ok(await _account.RemoverClaimUser(AdminDTO));
        }
    }
}
