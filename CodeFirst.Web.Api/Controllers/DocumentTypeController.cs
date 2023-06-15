using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Entities;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeFirst.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {

        private readonly IDocumentTypeService _documentTypeService;
        private readonly ISqlExampleService _sqlExampleService;
        public DocumentTypeController(IDocumentTypeService DocumentTypeService, ISqlExampleService sqlExampleService)
        {
            _documentTypeService = DocumentTypeService;
            _sqlExampleService = sqlExampleService;
        }

        /// <summary>
        /// Obtiene un los datos de tipo de documento (Entity Framework)
        /// </summary>
        /// <remarks>
        /// Los datos del tipo documento se obtiene por su Id.  (Entity Framework)
        /// </remarks>
        /// <returns>Retorna los datos del tipo documento solicitado.  (Entity Framework) </returns>
        /// <param name="id">Identificador del objeto curso.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("typedocument/{id:int}", Name = "documentById")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(await _documentTypeService.GetTypeDocumentAsync(id).ConfigureAwait(false));
        }


        /// <summary>
        /// Obtiene un los datos de tipo de documento (SQL)
        /// </summary>
        /// <remarks>
        /// Los datos del tipo documento se obtiene por su Id.  (SQL)
        /// </remarks>
        /// <returns>Retorna los datos del tipo documento solicitado.  (Entity Framework) </returns>
        /// <param name="id">Identificador del objeto curso.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("sqlexample/{id:int}", Name = "sqlExample")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSQL([FromRoute] long id)
        {
            return Ok(await _sqlExampleService.GetSQLResult());
        }



    }
}
