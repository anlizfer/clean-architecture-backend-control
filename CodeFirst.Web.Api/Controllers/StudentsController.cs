using CodeFirst.Core.DTOs.Student.Request;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CodeFirst.Web.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/student")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _student;
        public StudentsController(IStudentService student)
        {
            _student = student;
        }

        /// <summary>
        /// Obtiene un listado de Alumnos.
        /// </summary>
        /// <remarks>
        /// De acuerdo a los filtros se obtiene un listado de alumnos.
        /// </remarks>
        /// <returns>Retorna un listado de alumnos solicitado.</returns>
        /// <param name="filters">Diferentes filtros para obtener el listado de alumnos.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("listado", Name = nameof(GetAllAlumno))]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAlumno([FromQuery] StudentQueryFilter filters)
        {
            var students = _student.GetStudents(filters, Url.RouteUrl(nameof(GetAllAlumno)).ToString());
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(students.Meta));
            return Ok(students);
        }

        /// <summary>
        /// Obtiene un los datos de un alumno.
        /// </summary>
        /// <remarks>
        /// Los datos del alumno se obtiene por su Id.
        /// </remarks>
        /// <returns>Retorna los datos del alumno solicitado.</returns>
        /// <param name="id">Identificador del objeto Alumno.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("alumno/{id:int}", Name = "alumnoById")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(await _student.GetStudentAsync(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Agrega un nuevo alumno.
        /// </summary>
        /// <remarks>
        /// Inserta los datos del alumno.
        /// </remarks>
        /// <returns>Retorna los datos del alumno agregado.</returns>
        /// <param name="alumno">El objeto Alumno.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("alumno")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] StudentAddDtoRequest alumno)
        {
            return Ok(await _student.AddStudentAsync(alumno));
        }

        /// <summary>
        /// Actualiza el Alumno.
        /// </summary>
        /// <remarks>
        /// Actualiza los diferentes datos del alumno.
        /// </remarks>
        /// <returns>Retorna el alumno actualizado</returns>
        /// <param name="id">El identificador del alumno</param>
        /// <param name="alumno">Los datos del Alumno.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPut("alumno/{id:int}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] StudentUpdateDtoRequest alumno)
        {
            return Ok(await _student.UpdateStudentAsync(id, alumno));
        }

        /// <summary>
        /// Elimina el Alumno.
        /// </summary>
        /// <remarks>
        /// Elimina los dastos del  alumno po su id.
        /// </remarks>
        /// <returns>Retorna el Objeto alumno solicitado</returns>
        /// <param name="id">El identificador del alumno.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpDelete("alumno/{id:int}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            return Ok(await _student.DeleteStudentAsync(id));
        }
    }
}