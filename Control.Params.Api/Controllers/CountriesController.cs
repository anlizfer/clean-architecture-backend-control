using Control.Core.DTOs.Countries.Request;
using Control.Core.Interfaces.Services;
using Control.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Control.Params.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        readonly ICountriesService _CountriesService;
        public CountriesController(ICountriesService CountriesService)
        {
            _CountriesService = CountriesService;
        }

        /// <summary>
        /// Obtiene un los datos de tipo de país (EF)
        /// </summary>
        /// <remarks>
        /// Los datos de los paises se obtiene por su Id.  (EF)
        /// </remarks>
        /// <returns>Retorna los datos del país solicitado.  (Entity Framework) </returns>
        /// <param name="id">Identificador del objeto país.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("country", Name = "countryById")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        
        public async Task<IActionResult> Get()
        {
            return Ok(await _CountriesService.GetCountry().ConfigureAwait(false));
        }




    }
}
