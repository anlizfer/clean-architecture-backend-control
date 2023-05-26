using CodeFirst.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeFirst.Web.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/student")]
    public class DataProtectionController : ControllerBase
    {
        private readonly IDataProtectionService _dataProtection;
        public DataProtectionController(IDataProtectionService dataProtection)
        {
            _dataProtection = dataProtection;
        }

        /// <summary>
        /// Obtiene el Hash de un texto.
        /// </summary>
        /// <remarks>
        /// Se establece los Hash para password a partir de un texto.
        /// </remarks>
        /// <returns>Retorna el Hash de un texto.</returns>
        /// <param name="textoPlano">El texto a realizar Hash.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("hash/{textoPlano}")]
        public async Task<IActionResult> RealizarHash(string textoPlano)
        {
            return Ok(await _dataProtection.RealizarHash(textoPlano));
        }

        /// <summary>
        /// Encripta y desencripta un texto.
        /// </summary>
        /// <remarks>
        /// Los texto para encriptar y descencriptar paral protección de datos.
        /// </remarks>
        /// <returns>Retorna El texto encriptado y desencriptado.</returns>
        /// <param name="textoPlano">Texto a encriptar y desencriptar.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("encriptar")]
        public async Task<IActionResult> Encriptar(string textoPlano)
        {
            return Ok(await _dataProtection.Encriptar(textoPlano));
        }
    }
}
